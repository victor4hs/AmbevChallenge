using Ambev.DeveloperEvaluation.Application.SalesItem.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Represents the response model for a sales operation, containing details about a sale.
/// </summary>
public class SaleResult
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
