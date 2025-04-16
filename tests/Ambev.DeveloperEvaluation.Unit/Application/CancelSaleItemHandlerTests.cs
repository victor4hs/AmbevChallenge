using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.SalesItem.CancelSaleItem;
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
/// Contains unit tests for the <see cref="CancelSaleItemHandlerTests"/> class.
/// </summary>
public class CancelSaleItemHandlerTests
{
    private readonly ISalesItemRepository _salesRepository = Substitute.For<ISalesItemRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IPublisher _eventPublisher = Substitute.For<IPublisher>();
    private readonly CancelSaleItemHandler _handler;

    public CancelSaleItemHandlerTests()
    {
        _handler = new CancelSaleItemHandler(_salesRepository, _mapper, _eventPublisher);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSaleDoesNotExist()
    {
        // Arrange
        var command = new CancelSaleItemCommand(Guid.NewGuid());
        _salesRepository.GetByIdAsync(command.Id).Returns((SaleItem)null);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"SaleItem with ID {command.Id} not found.");
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<SaleItem>());
        await _eventPublisher.DidNotReceive().Publish(Arg.Any<ItemCancelledEvent>());
    }

    [Fact]
    public async Task Handle_ShouldReturnItemAlreadyCanceled_WhenSaleItemExist()
    {
        // Arrange
        var saleItem = CreateSaleItemHandlerTestData.Create();
        saleItem.Cancel();
        var command = new CancelSaleItemCommand(Guid.NewGuid());
        _salesRepository.GetByIdAsync(command.Id).Returns(saleItem);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"SaleItem with ID {command.Id} already canceled.");
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<SaleItem>());
        await _eventPublisher.DidNotReceive().Publish(Arg.Any<ItemCancelledEvent>());
    }

    [Fact]
    public async Task Handle_ShouldCancelSaleSuccessfully_WhenSaleExists()
    {
        // Arrange
        var command = new CancelSaleItemCommand(Guid.NewGuid());
        var saleItem = CreateSaleItemHandlerTestData.Create();

        _salesRepository.GetByIdAsync(command.Id).Returns(saleItem);
        _salesRepository.UpdateAsync(Arg.Any<SaleItem>()).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(saleItem.IsCancelled);

        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.Received(1).UpdateAsync(saleItem);
        await _eventPublisher.Received(1).Publish(Arg.Is<ItemCancelledEvent>(e => e.SaleItem == saleItem));
    }

}
