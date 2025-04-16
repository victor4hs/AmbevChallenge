using Ambev.DeveloperEvaluation.Application.SalesItem.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.CancelSaleItem;

/// <summary>
/// Command for canceling a sale item by its ID
/// </summary>
public record CancelSaleItemCommand : IRequest<SaleItemResult>
{
    /// <summary>
    /// The unique identifier of the sale item to cancel
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of CancelSaleItemCommand
    /// </summary>
    /// <param name="id">The ID of the sale item to cancel</param>
    public CancelSaleItemCommand(Guid id)
    {
        Id = id;
    }
}
