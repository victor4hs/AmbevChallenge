using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for the sale creation command.
/// </summary>
public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - <see cref="CreateSaleCommand.BranchId"/>: Must not be empty.
    /// - <see cref="CreateSaleCommand.CustomerId"/>: Must not be empty.
    /// </remarks>
    public CreateSaleItemCommandValidator()
    {
        RuleFor(sale => sale.ProductId).NotEmpty();
        RuleFor(sale => sale.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero");
        RuleFor(sale => sale.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero")
            .LessThanOrEqualTo(20).WithMessage("Quantity must not exceed 20");
    }
}