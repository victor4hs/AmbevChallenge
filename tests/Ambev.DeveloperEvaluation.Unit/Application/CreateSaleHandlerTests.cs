using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
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
/// Contains unit tests for the <see cref="CreateSaleHandlerTests"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISalesRepository _salesRepository = Substitute.For<ISalesRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IPublisher _eventPublisher = Substitute.For<IPublisher>();
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<SaleProfile>());
        _mapper = configuration.CreateMapper();
        _handler = new CreateSaleHandler(_salesRepository, _mapper, _eventPublisher);
    }

    [Fact]
    public async Task Handle_ShouldCreateSaleSuccessfully_WhenSaleDoesNotExists()
    {
        // Arrange
        var sale = CreateSaleHandlerTestData.Create();
        var command = CreateSaleHandlerTestData.CreateCommand();

        _salesRepository.CreateAsync(Arg.Any<Sale>()).Returns(sale);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        await _salesRepository.Received(1).CreateAsync(Arg.Any<Sale>());
        await _eventPublisher.Received(1).Publish(Arg.Any<SaleCreatedEvent>());
    }

}
