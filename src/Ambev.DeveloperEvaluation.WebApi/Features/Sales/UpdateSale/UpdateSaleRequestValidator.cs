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
        RuleFor(sale => sale.Id).NotEmpty()
            .WithMessage("Id must be informated.");
    }
}