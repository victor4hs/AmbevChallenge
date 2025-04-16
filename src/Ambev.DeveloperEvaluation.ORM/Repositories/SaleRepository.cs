using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.ORM.Context.PostgreSQL;
using Ambev.DeveloperEvaluation.ORM.Helper;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of <see cref="ISalesRepository"/> using Entity Framework Core.
/// </summary>
public class SaleRepository : ISalesRepository
{
    private readonly PostgreSQLContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public SaleRepository(PostgreSQLContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new sale in the database.
    /// </summary>
    /// <param name="sale">The sale entity to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The created sale entity.</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="queryFilter">The query of the sale.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The sale entity if found, or null otherwise.</returns>
    public async Task<IEnumerable<Sale>> GetAllAsync(SaleQueryFilter queryFilter, CancellationToken cancellationToken = default)
    {
        var query = _context.Sales.AsQueryable();

        query = FilterHelper.CreateFilter(query, queryFilter);
        query = FilterHelper.ApplySorting(query, queryFilter.SortField, queryFilter.SortOrder);
        
        int skip = (queryFilter.PageNumber - 1) * queryFilter.PageSize;
        query = query.Skip(skip).Take(queryFilter.PageSize);

        return await query
            .Include(c => c.SaleItems)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The sale entity if found, or null otherwise.</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(c => c.SaleItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing sale in the database.
    /// </summary>
    /// <param name="sale">The sale entity with updated information.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated sale entity.</returns>
    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }
}