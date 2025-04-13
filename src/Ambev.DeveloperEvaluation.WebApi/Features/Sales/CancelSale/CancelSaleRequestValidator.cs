using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Validator for <see cref="CancelSaleRequest"/> that defines validation rules for canceling a sale.
/// </summary>
public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    /// <summary>
    /// Initializes validation rules for <see cref="CancelSaleRequest"/>.
    /// </summary>
    public CancelSaleRequestValidator()
    {
        // Ensures that the Sale ID is provided and not empty.
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}