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
        // Ensures that the BranchId is provided.
        RuleFor(sale => sale.BranchId).NotEmpty();

        // Ensures that the CustomerId is provided.
        RuleFor(sale => sale.CustomerId).NotEmpty();
    }
}