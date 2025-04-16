using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateSaleResult
{
    /// <summary>
    /// Unique identifier for the sale (e.g., invoice number).
    /// </summary>
    public Guid BranchId { get; private set; }

    /// <summary>
    /// Unique identifier for the sale (e.g., invoice number).
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Unique identifier for the sale (e.g., invoice number).
    /// </summary>
    public string SaleNumber { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the newly created user.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created user in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// The total amount of the sale, including all items and discounts.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// The total amount of the sale, including all items and discounts.
    /// </summary>
    public ICollection<CreateSaleItemResult> SaleItems { get; private set; }
}
