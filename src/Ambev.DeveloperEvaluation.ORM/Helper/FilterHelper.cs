using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.ORM.Helper;

/// <summary>
/// Provides helper methods to filter and sort sales data in a queryable format.
/// </summary>
public class FilterHelper
{
    /// <summary>
    /// Applies filtering criteria to a queryable collection of sales based on the provided filter object.
    /// </summary>
    /// <param name="query">The queryable collection of sales to filter.</param>
    /// <param name="filter">The filter object containing the filtering criteria.</param>
    /// <returns>A filtered queryable collection of sales.</returns>
    public static IQueryable<Sale> CreateFilter(IQueryable<Sale> query, SaleQueryFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.SaleNumber))
            query = query.Where(x => x.SaleNumber == filter.SaleNumber);

        if (filter.SaleDateFrom.HasValue)
            query = query.Where(x => x.SaleDate >= filter.SaleDateFrom.Value);

        if (filter.SaleDateTo.HasValue)
            query = query.Where(x => x.SaleDate <= filter.SaleDateTo.Value);

        if (filter.CustomerId.HasValue)
            query = query.Where(x => x.CustomerId == filter.CustomerId.Value);

        if (filter.BranchId.HasValue)
            query = query.Where(x => x.BranchId == filter.BranchId.Value);

        if (filter.IsCancelled.HasValue)
            query = query.Where(x => x.IsCancelled == filter.IsCancelled.Value);

        if (filter.MinTotalAmount.HasValue)
            query = query.Where(x => x.TotalAmount >= filter.MinTotalAmount.Value);

        if (filter.MaxTotalAmount.HasValue)
            query = query.Where(x => x.TotalAmount <= filter.MaxTotalAmount.Value);

        return query;
    }

    /// <summary>
    /// Applies sorting to a queryable collection of sales based on the specified sort field and order.
    /// </summary>
    /// <param name="query">The queryable collection of sales to sort.</param>
    /// <param name="sortBy">The field by which to sort (e.g., "SaleDate", "CustomerName").</param>
    /// <param name="sortOrder">The sort order ("asc" for ascending, "desc" for descending).</param>
    /// <returns>A sorted queryable collection of sales.</returns>
    public static IQueryable<Sale> ApplySorting(IQueryable<Sale> query, string sortBy, string sortOrder)
    {
        bool isDescending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);

        return sortBy.ToLower() switch
        {
            "saledate" => isDescending ? query.OrderByDescending(x => x.SaleDate) : query.OrderBy(x => x.SaleDate),
            "totalamount" => isDescending ? query.OrderByDescending(x => x.TotalAmount) : query.OrderBy(x => x.TotalAmount),
            _ => query.OrderByDescending(x => x.SaleDate),
        };
    }
}