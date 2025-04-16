using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.GetSaleItem;

/// <summary>
/// Validator for GetSalesItemRequest
/// </summary>
public class GetSaleItemRequestValidator : AbstractValidator<GetSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSalesItemRequest
    /// </summary>
    public GetSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sales ID is required");
    }
}
