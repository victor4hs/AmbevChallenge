namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.GetSaleItem;

/// <summary>
/// Request model for getting a Sales by ID
/// </summary>
public class GetSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the Sales to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
