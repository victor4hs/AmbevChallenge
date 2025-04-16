namespace Ambev.DeveloperEvaluation.Application.SalesItem.Common;

/// <summary>
/// Represents the details of an individual item in a sale.
/// </summary>
public class SaleItemResult
{
    /// <summary>
    /// The unique identifier of the sale item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the sale to which this item belongs.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Information about the product associated with this sale item.
    /// </summary>
    public ProductInfo Product { get; set; }

    /// <summary>
    /// The quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The discount applied to this sale item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The price unit
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The total price of this sale item after applying the discount.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Indicates whether this sale item has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}

/// <summary>
/// Represents information about a product included in a sale.
/// </summary>
public class ProductInfo
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }
}

