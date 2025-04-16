using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleItemRequest"/> that defines validation rules for sale item creation.
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleItemRequestValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: Must not be empty.
    /// - UnitPrice: Must not be empty.
    /// - Quantity: Must not be empty.
    /// </remarks>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(sales => sales.ProductId).NotEmpty();
        RuleFor(sale => sale.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero");
        RuleFor(sale => sale.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");
    }
}