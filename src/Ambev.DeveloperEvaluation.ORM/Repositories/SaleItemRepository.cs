using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.ORM.Context.PostgreSQL;
using Ambev.DeveloperEvaluation.ORM.Helper;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of <see cref="ISalesItemRepository"/> using Entity Framework Core.
/// </summary>
public class SaleItemRepository : ISalesItemRepository
{
    private readonly PostgreSQLContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItemRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public SaleItemRepository(PostgreSQLContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The sale entity if found, or null otherwise.</returns>
    public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing sale in the database.
    /// </summary>
    /// <param name="saleItem">The sale entity with updated information.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated sale entity.</returns>
    public async Task UpdateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        _context.SaleItems.Update(saleItem);
        await _context.SaveChangesAsync(cancellationToken);
    }
}