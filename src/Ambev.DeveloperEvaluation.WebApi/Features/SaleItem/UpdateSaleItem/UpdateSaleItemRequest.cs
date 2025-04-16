namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.UpdateSaleItem;

/// <summary>
/// Represents a request to update a new sale in the system.
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// Unique identifier for the sale (e.g., invoice number).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product name associated with this sale item.
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// Identifier of the product being sold.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of the product being sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price of the product at the time of sale.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Indicates whether this sale item has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}