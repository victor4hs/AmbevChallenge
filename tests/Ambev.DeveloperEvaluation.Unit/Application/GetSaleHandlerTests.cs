using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleHandlerTests"/> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISalesRepository _salesRepository = Substitute.For<ISalesRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _handler = new GetSaleHandler(_salesRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenSaleDoesNotExist()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.NewGuid());
        _salesRepository.GetByIdAsync(command.Id).Returns((Sale)null);

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.Message.Should().Be($"Sale with ID {command.Id} not found.");
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
        await _salesRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>());
    }

    [Fact]
    public async Task Handle_ShouldGetSaleSuccessfully_WhenSaleExists()
    {
        // Arrange
        var command = new GetSaleCommand(Guid.NewGuid());
        var sale = CreateSaleHandlerTestData.Create();

        _salesRepository.GetByIdAsync(command.Id).Returns(sale);
        _mapper.Map<SaleResult>(sale).Returns(new SaleResult
        {
            Id = sale.Id,
        });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(sale);
        Assert.Equal(sale?.Id, result.Id);
        await _salesRepository.Received(1).GetByIdAsync(command.Id);
    }

}
