using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleRequest"/> that defines validation rules for sales creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleRequestValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleItems: Each item must pass the <see cref="CreateSaleItemRequestValidator"/>.
    /// - BranchId: Must not be null.
    /// - CustomerId: Must not be null.
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        // Validates each sale item using the CreateSaleItemRequestValidator.
        RuleForEach(sales => sales.SaleItems).SetValidator(new CreateSaleItemRequestValidator());

        // Ensures that the BranchId is provided.
        RuleFor(sales => sales.BranchId).NotNull();

        // Ensures that the CustomerId is provided.
        RuleFor(sales => sales.CustomerId).NotNull();
    }
}