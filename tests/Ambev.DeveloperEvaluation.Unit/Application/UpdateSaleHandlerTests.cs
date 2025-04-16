using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
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
/// Contains unit tests for the <see cref="UpdateSaleHandlerTests"/> class.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly ISalesRepository _salesRepository = Substitute.For<ISalesRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IPublisher _eventPublisher = Substitute.For<IPublisher>();
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<SaleProfile>());
        _mapper = configuration.CreateMapper();
        _handler = new UpdateSaleHandler(_salesRepository, _mapper, _eventPublisher);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSaleDoesNotExist()
    {
        // Arrange
        var sale = CreateSaleHandlerTestData.Create();
        var command = CreateSaleHandlerTestData.UpdateCommand();
        _salesRepository.GetByIdAsync(command.Id).Returns((Sale)null);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"Sale with ID {command.Id} not found.");
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>());
        await _eventPublisher.DidNotReceive().Publish(Arg.Any<SaleCancelledEvent>());
    }

    [Fact]
    public async Task Handle_ShouldUpdateSaleSuccessfully_WhenSaleExists()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.UpdateCommand();
        var sale = CreateSaleHandlerTestData.Create();

        _salesRepository.GetByIdAsync(command.Id).Returns(sale);
        _salesRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.Received(1).UpdateAsync(sale);
        await _eventPublisher.Received(1).Publish(Arg.Any<SaleModifiedEvent>());
    }

}
