using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.CancelSale;

/// <summary>
/// Validator for <see cref="CancelSaleItemRequest"/> that defines validation rules for canceling a sale.
/// </summary>
public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for <see cref="CancelSaleItemRequest"/>.
    /// </summary>
    public CancelSaleItemRequestValidator()
    {
        // Ensures that the Sale ID is provided and not empty.
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}