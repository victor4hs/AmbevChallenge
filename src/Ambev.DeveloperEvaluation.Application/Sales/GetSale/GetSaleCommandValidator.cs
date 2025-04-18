using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Validator for <see cref="GetSaleCommand"/> that defines validation rules for retrieving a sale.
/// </summary>
public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for <see cref="GetSaleCommand"/>.
    /// </summary>
    public GetSaleCommandValidator()
    {
        // Ensures that the Sale ID is provided and not empty.
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}