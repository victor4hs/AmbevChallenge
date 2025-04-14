namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a new sale in the system.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// The date and time when the sale was made.
    /// </summary>
    public DateTime? SaleDate { get; private set; }

    /// <summary>
    /// Identifier of the customer who made the purchase.
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Identifier of the branch where the sale was made.
    /// </summary>
    public Guid BranchId { get; private set; }

    /// <summary>
    /// The collection of items included in the sale.
    /// </summary>
    public ICollection<UpdateSaleItemRequest> SaleItems { get; private set; }
}