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
public static class CreateSaleItemHandlerTestData
{
    private static readonly Faker<SaleItem> _factory = new Faker<SaleItem>()
        .RuleFor(i => i.Id, f => Guid.NewGuid())
        .RuleFor(i => i.SaleId, f => Guid.NewGuid())
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Int(1, 10))
        .RuleFor(i => i.IsCancelled, f => f.Random.Bool(0.1f));

    private static readonly Faker<UpdateSaleItemCommand> _factoryUpdate = new Faker<UpdateSaleItemCommand>()
        .RuleFor(i => i.Id, f => Guid.NewGuid())
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Int(1, 10))
        .RuleFor(i => i.IsCancelled, f => f.Random.Bool(0.1f));

    static CreateSaleItemHandlerTestData()
    {
        _factory.FinishWith((f, item) =>
        {
            decimal totalPrice = item.UnitPrice * item.Quantity;
            if (item.Discount > 0)
            {
                totalPrice -= (totalPrice * item.Discount / 100);
            }
            item.UpdateFrom(Math.Round(totalPrice, 2));
        });
    }

    public static SaleItem Create()
    {
        return _factory.Generate();
    }

    public static UpdateSaleItemCommand UpdateCommand()
    {
        return _factoryUpdate.Generate();
    }

    public static SaleItem Create(Action<SaleItem> customizer)
    {
        var item = _factory.Generate();
        customizer(item);
        return item;
    }

    public static IEnumerable<SaleItem> CreateMany(int count)
    {
        return _factory.Generate(count);
    }

    public static IEnumerable<SaleItem> CreateMany(int count, Action<SaleItem> customizer)
    {
        var items = _factory.Generate(count);
        foreach (var item in items)
        {
            customizer(item);
        }
        return items;
    }
}
