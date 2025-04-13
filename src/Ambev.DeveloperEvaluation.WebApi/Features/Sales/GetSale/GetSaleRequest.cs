namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Request model for getting a Sales by ID
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    /// The unique identifier of the Sales to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
