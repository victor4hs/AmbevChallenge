using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleRequest"/> that defines validation rules for sales update.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleRequestValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleItems: Each item must pass the <see cref="UpdateSaleItemRequestValidator"/>.
    /// </remarks>
    public UpdateSaleRequestValidator()
    {
        // Validates each sale item using the UpdateSaleItemRequestValidator.
        RuleForEach(sales => sales.SaleItems).SetValidator(new UpdateSaleItemRequestValidator());
    }
}