using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for GetSalesRequest
/// </summary>
public class GetAllSaleRequestValidator : AbstractValidator<GetAllSalesQuery>
{
    /// <summary>
    /// Initializes validation rules for GetSalesRequest
    /// </summary>
    public GetAllSaleRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number cannot be less than 1");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size cannot be less than 1");
    }
}
