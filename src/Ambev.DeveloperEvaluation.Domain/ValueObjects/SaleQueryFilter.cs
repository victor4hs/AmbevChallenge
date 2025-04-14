namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents the filtering and sorting criteria for querying sales data.
/// </summary>
public class SaleQueryFilter
{
    /// <summary>
    /// Gets or sets the sale number to filter by.
    /// </summary>
    public string? SaleNumber { get; set; }

    /// <summary>
    /// Gets or sets the start date for filtering sales.
    /// </summary>
    public DateTime? SaleDateFrom { get; set; }

    /// <summary>
    /// Gets or sets the end date for filtering sales.
    /// </summary>
    public DateTime? SaleDateTo { get; set; }

    /// <summary>
    /// Gets or sets the customer ID to filter by.
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the branch ID to filter by.
    /// </summary>
    public Guid? BranchId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to filter by cancelled sales.
    /// </summary>
    public bool? IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the minimum total amount for filtering sales.
    /// </summary>
    public decimal? MinTotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the maximum total amount for filtering sales.
    /// </summary>
    public decimal? MaxTotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the page number for pagination.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the page size for pagination.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the field by which to sort the sales data.
    /// </summary>
    public required string SortField { get; set; }

    /// <summary>
    /// Gets or sets the sort order ("asc" for ascending, "desc" for descending).
    /// </summary>
    public required string SortOrder { get; set; }
}