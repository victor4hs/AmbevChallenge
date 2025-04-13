namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Request model for canceling a sale by its ID.
/// </summary>
public class CancelSaleRequest
{
    /// <summary>
    /// The unique identifier of the sale to cancel.
    /// </summary>
    public Guid Id { get; set; }
}