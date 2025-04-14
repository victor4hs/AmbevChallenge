using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for retrieving a sale by its ID.
/// </summary>
public record GetAllSaleCommand : IRequest<List<SaleResult>>
{

    /// <summary>
    /// The unique identifier of the sale to cancel
    /// </summary>
    public GetAllSalesQuery GetAllSalesQuery { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="GetSaleCommand"/>.
    /// </summary>
    /// <param name="id">The ID of the sale to retrieve.</param>
    public GetAllSaleCommand(GetAllSalesQuery getAllSalesQuery) 
    {
        GetAllSalesQuery = getAllSalesQuery;
    }
}