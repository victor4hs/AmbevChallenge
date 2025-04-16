using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    private static readonly Faker<Sale> Factory = new Faker<Sale>()
         .RuleFor(d => d.Id, f => Guid.NewGuid())
         .RuleFor(d => d.SaleNumber, f => f.Commerce.Product() + "-" + f.Random.Number(1000, 9999))
         .RuleFor(d => d.SaleDate, f => f.Date.Recent(30))
         .RuleFor(d => d.CustomerId, f => Guid.NewGuid())
         .RuleFor(d => d.BranchId, f => Guid.NewGuid())
         .RuleFor(d => d.TotalAmount, f => f.Random.Decimal(100, 5000))
         .RuleFor(d => d.IsCancelled, f => f.Random.Bool(0.1f))
         .RuleFor(d => d.SaleItems, f => new List<SaleItem>());

    private static readonly Faker<CreateSaleCommand> FactoryCommand = new Faker<CreateSaleCommand>()
         .RuleFor(d => d.SaleDate, f => f.Date.Recent(30))
         .RuleFor(d => d.CustomerId, f => Guid.NewGuid())
         .RuleFor(d => d.BranchId, f => Guid.NewGuid())
         .RuleFor(d => d.SaleItems, f => new List<CreateSaleItemCommand>());

    private static readonly Faker<UpdateSaleCommand> FactoryUpdate = new Faker<UpdateSaleCommand>()
        .RuleFor(d => d.Id, f => Guid.NewGuid())
        .RuleFor(d => d.SaleDate, f => f.Date.Recent(30))
         .RuleFor(d => d.CustomerId, f => Guid.NewGuid())
         .RuleFor(d => d.BranchId, f => Guid.NewGuid());

    public static Sale Create()
    {
        return Factory.Generate();
    }

    public static CreateSaleCommand CreateCommand()
    {
        return FactoryCommand.Generate();
    }

    public static UpdateSaleCommand UpdateCommand()
    {
        return FactoryUpdate.Generate();
    }

    public static Sale Create(Action<Sale> customizer)
    {
        var document = Factory.Generate();
        customizer(document);
        return document;
    }

    public static Sale WithItems(this Sale document, int count = 3)
    {
        var list = CreateSaleItemHandlerTestData.CreateMany(count, item =>
        {
            new Sale();
        }).ToList();

        document.UpdateSaleItem(list);

        return document;
    }
}
