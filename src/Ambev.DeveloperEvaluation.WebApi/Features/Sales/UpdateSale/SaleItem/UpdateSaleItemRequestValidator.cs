using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleItemRequest"/> that defines validation rules for sale item updated.
/// </summary>
public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleItemRequestValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Quantity: Must not be less than 1.
    /// </remarks>
    public UpdateSaleItemRequestValidator()
    {
        // Ensures that the Quantity is provided.
        RuleFor(sales => sales.Quantity).LessThan(1);
    }
}