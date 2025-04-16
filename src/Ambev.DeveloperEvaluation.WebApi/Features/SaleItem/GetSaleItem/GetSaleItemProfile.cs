using Ambev.DeveloperEvaluation.Application.SalesItem.GetSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.GetSaleItem;

/// <summary>
/// Profile for mapping GetSaleItemProfile feature requests to commands
/// </summary>
public class GetSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleItemProfile feature
    /// </summary>
    public GetSaleItemProfile()
    {
        CreateMap<Guid, GetSaleItemCommand>()
            .ConstructUsing(id => new GetSaleItemCommand(id));
    }
}
