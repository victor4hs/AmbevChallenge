using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.GetSaleItem;

/// <summary>
/// Validator for <see cref="GetSaleItemCommand"/> that defines validation rules for retrieving a sale item.
/// </summary>
public class GetSaleItemCommandValidator : AbstractValidator<GetSaleItemCommand>
{
    /// <summary>
    /// Initializes validation rules for <see cref="GetSaleItemCommand"/>.
    /// </summary>
    public GetSaleItemCommandValidator()
    {
        // Ensures that the Sale item ID is provided and not empty.
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}