using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"/> that defines validation rules for the sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - <see cref="CreateSaleCommand.BranchId"/>: Must not be empty.
    /// - <see cref="CreateSaleCommand.CustomerId"/>: Must not be empty.
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.BranchId).NotEmpty();
        RuleFor(sale => sale.CustomerId).NotEmpty();
        RuleForEach(sales => sales.SaleItems).SetValidator(new CreateSaleItemCommandValidator());
    }
}