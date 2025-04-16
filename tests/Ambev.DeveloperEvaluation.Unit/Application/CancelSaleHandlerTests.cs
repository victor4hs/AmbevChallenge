using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CancelSaleHandlerTests"/> class.
/// </summary>
public class CancelSaleHandlerTests
{
    private readonly ISalesRepository _salesRepository = Substitute.For<ISalesRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IPublisher _eventPublisher = Substitute.For<IPublisher>();
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _handler = new CancelSaleHandler(_salesRepository, _mapper, _eventPublisher);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSaleItemDoesNotExist()
    {
        // Arrange
        var command = new CancelSaleCommand(Guid.NewGuid());
        _salesRepository.GetByIdAsync(command.Id).Returns((Sale)null);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"SaleItem with ID {command.Id} not found.");
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>());
        await _eventPublisher.DidNotReceive().Publish(Arg.Any<SaleCancelledEvent>());
    }

    [Fact]
    public async Task Handle_ShouldReturnItemAlreadyCanceled_WhenSaleItemExist()
    {
        // Arrange
        var sale = CreateSaleHandlerTestData.Create();
        sale.Cancel();
        var command = new CancelSaleCommand(Guid.NewGuid());
        _salesRepository.GetByIdAsync(command.Id).Returns(sale);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"Sale with ID {command.Id} already canceled.");
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>());
        await _eventPublisher.DidNotReceive().Publish(Arg.Any<SaleCancelledEvent>());
    }

    [Fact]
    public async Task Handle_ShouldCancelSaleSuccessfully_WhenSaleItemExists()
    {
        // Arrange
        var command = new CancelSaleCommand(Guid.NewGuid());
        var sale = CreateSaleHandlerTestData.Create();

        _salesRepository.GetByIdAsync(command.Id).Returns(sale);
        _salesRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(sale.IsCancelled);

        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.Received(1).UpdateAsync(sale);
        await _eventPublisher.Received(1).Publish(Arg.Is<SaleCancelledEvent>(e => e.Sale == sale));
    }

}
