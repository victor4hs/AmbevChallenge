namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateSaleItemResult
{
    /// <summary>
    /// Unique identifier for the sale (e.g., invoice number).
    /// </summary>
    public Guid SaleId { get; private set; }

    /// <summary>
    /// Identifier of the product being sold.
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
    /// Discount applied to this sale item.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// Total price for this sale item after applying discounts.
    /// </summary>
    public decimal TotalPrice { get; private set; }

    /// <summary>
    /// Indicates whether this sale item has been cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }
}
