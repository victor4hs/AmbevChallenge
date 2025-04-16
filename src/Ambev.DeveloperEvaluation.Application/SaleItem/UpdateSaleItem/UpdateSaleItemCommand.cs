using Ambev.DeveloperEvaluation.Application.SalesItem.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;

/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a sale, 
/// including the sale date, customer ID, branch ID, and the items included in the sale. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="SaleItemResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateSaleItemCommand"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateSaleItemCommand : IRequest<SaleItemResult>
{
    /// <summary>
    /// Unique identifier for the sale (e.g., invoice number).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product name associated with this sale item.
    /// </summary>
    public string? ProductName { get; private set; }

    /// <summary>
    /// Identifier of the product being sold.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Quantity of the product being sold.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Unit price of the product at the time of sale.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Indicates whether this sale item has been cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Validates the command using the <see cref="UpdateSaleItemCommandValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> object containing the validation result 
    /// and any validation errors.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}