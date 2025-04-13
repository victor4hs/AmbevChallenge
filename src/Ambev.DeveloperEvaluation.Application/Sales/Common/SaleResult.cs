using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Represents the response model for a sales operation, containing details about a sale.
/// </summary>
public class SaleResult : Result<SaleResult>
{
    /// <summary>
    /// The unique identifier of the sale.
    /// </summary>
    public virtual Guid Id { get; set; }

    /// <summary>
    /// The sale number, used to uniquely identify the sale.
    /// </summary>
    public string SaleNumber { get; set; }

    /// <summary>
    /// The date when the sale occurred.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The unique identifier of the customer associated with the sale.
    /// </summary>
    public string CustomerId { get; set; }

    /// <summary>
    /// The unique identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The total amount of the sale, including all items and discounts.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Indicates whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// A list of items included in the sale.
    /// </summary>
    public List<SaleItemResult> SaleItems { get; set; }
}

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