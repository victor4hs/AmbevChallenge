using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.UpdateSaleItem;

/// <summary>
/// Validator for <see cref="UpdateSaleItemRequest"/> that defines validation rules for sales update.
/// </summary>
public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleItemRequestValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleItems: Each item must pass the <see cref="UpdateSaleItemRequestValidator"/>.
    /// </remarks>
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(sale => sale.Id).NotEmpty()
            .WithMessage("Id must be informated.");
    }
}