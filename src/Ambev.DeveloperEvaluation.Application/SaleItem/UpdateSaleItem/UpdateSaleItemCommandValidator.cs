using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;

/// <summary>
/// Validator for <see cref="UpdateSaleItemCommand"/> that defines validation rules for the sale update command.
/// </summary>
public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleItemCommandValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - <see cref="UpdateSaleItemCommand.BranchId"/>: Must not be empty.
    /// - <see cref="UpdateSaleItemCommand.CustomerId"/>: Must not be empty.
    /// </remarks>
    public UpdateSaleItemCommandValidator()
    {
        RuleFor(sale => sale.Id).NotEmpty();
        RuleFor(sale => sale.ProductId).NotEmpty();
        RuleFor(sale => sale.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than zero");
        RuleFor(sale => sale.Quantity).LessThanOrEqualTo(20).WithMessage("Maximum 20 items per product");
    }
}