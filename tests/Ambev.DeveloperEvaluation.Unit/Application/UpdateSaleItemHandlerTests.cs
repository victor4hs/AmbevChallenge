using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.SalesItem.Common;
using Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;
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
public class UpdateSaleItemHandlerTests
{
    private readonly ISalesRepository _salesRepository = Substitute.For<ISalesRepository>();
    private readonly ISalesItemRepository _salesItemRepository = Substitute.For<ISalesItemRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly UpdateSaleItemHandler _handler;

    public UpdateSaleItemHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SaleProfile>();
            cfg.AddProfile<SaleItemProfile>();
        });
        _mapper = configuration.CreateMapper();
        _handler = new UpdateSaleItemHandler(_salesRepository, _mapper, _salesItemRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSaleItemDoesNotExist()
    {
        // Arrange
        var commandItem = CreateSaleItemHandlerTestData.UpdateCommand();
        _salesItemRepository.GetByIdAsync(commandItem.Id).Returns((SaleItem)null);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(commandItem, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"SaleItem with ID {commandItem.Id} not found.");
        await _salesItemRepository.Received(1).GetByIdAsync(commandItem.Id);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>());
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSaleDoesNotExist()
    {
        // Arrange
        var saleItem = CreateSaleItemHandlerTestData.Create();
        var commandItem = CreateSaleItemHandlerTestData.UpdateCommand();
        
        _salesRepository.GetByIdAsync(saleItem.SaleId).Returns((Sale)null);
        _salesItemRepository.GetByIdAsync(commandItem.Id).Returns(saleItem);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(commandItem, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"Sale with ID {commandItem.Id} not found.");
        await _salesItemRepository.Received(1).GetByIdAsync(commandItem.Id);
        await _salesRepository.Received(1).GetByIdAsync(saleItem.SaleId);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>());
    }

    [Fact]
    public async Task Handle_ShouldUpdateSaleItemSuccessfully_WhenSaleItemExists()
    {
        // Arrange
        var sale = CreateSaleHandlerTestData.Create();
        var saleItem = CreateSaleItemHandlerTestData.Create();
        var commandItem = CreateSaleItemHandlerTestData.UpdateCommand();

        _salesRepository.GetByIdAsync(saleItem.SaleId).Returns(sale);
        _salesItemRepository.GetByIdAsync(commandItem.Id).Returns(saleItem);
        _salesRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(commandItem, CancellationToken.None);

        // Assert
        await _salesItemRepository.Received(1).GetByIdAsync(commandItem.Id);
        await _salesRepository.Received(1).UpdateAsync(sale);
    }

}
