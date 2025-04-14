using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("Branch ID is required");

        RuleFor(sale => sale.SaleDate)
            .NotEmpty().WithMessage("Sale date is required");

        RuleFor(sale => sale.SaleItems)
            .NotEmpty().WithMessage("Sale must have at least one item");

        RuleFor(sale => sale.SaleItems)
            .Must(items =>
            {
                var activeItems = items.Where(x => !x.IsCancelled).ToList();
                if (activeItems.Count <= 1)
                    return true;
                return activeItems.Select(i => i.ProductId).Distinct().Count() == activeItems.Count;
            })
            .When(sale => sale.SaleItems != null && sale.SaleItems.Any())
            .WithMessage("Duplicate products are not allowed in a sale");

        RuleForEach(sale => sale.SaleItems)
            .SetValidator(new SaleItemValidator());
    }
}
