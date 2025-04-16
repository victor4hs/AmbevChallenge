using Ambev.DeveloperEvaluation.Application.SalesItem.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.GetSaleItem;

/// <summary>
/// Command for retrieving a sale item by its ID.
/// </summary>
public record GetSaleItemCommand : IRequest<SaleItemResult>
{
    /// <summary>
    /// The unique identifier of the sale item to retrieve.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="GetSaleItemCommand"/>.
    /// </summary>
    /// <param name="id">The ID of the sale item to retrieve.</param>
    public GetSaleItemCommand(Guid id)
    {
        Id = id;
    }
}