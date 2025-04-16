namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.CancelSale;

/// <summary>
/// Request model for canceling a sale by its ID.
/// </summary>
public class CancelSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the sale to cancel.
    /// </summary>
    public Guid Id { get; set; }
}