using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.CancelSaleItem;

/// <summary>
/// Validator for CancelSaleCommand
/// </summary>
public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
{
    /// <summary>
    /// Initializes validation rules for CancelSaleCommand
    /// </summary>
    public CancelSaleItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
