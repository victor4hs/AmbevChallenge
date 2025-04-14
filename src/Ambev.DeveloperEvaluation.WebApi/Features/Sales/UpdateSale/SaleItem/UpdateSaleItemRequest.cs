namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a new sale item in the system.
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the product associated with this sale item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product to be sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; private set; }
}