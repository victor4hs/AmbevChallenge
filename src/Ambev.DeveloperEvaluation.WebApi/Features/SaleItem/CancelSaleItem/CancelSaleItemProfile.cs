using Ambev.DeveloperEvaluation.Application.SalesItem.CancelSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.CancelSaleItem;

/// <summary>
/// Profile for mapping CancelItemSale feature requests to commands.
/// </summary>
public class CancelSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CancelItemSale feature.
    /// </summary>
    public CancelSaleItemProfile()
    {
        CreateMap<Guid, CancelSaleItemCommand>()
            .ConstructUsing(id => new CancelSaleItemCommand(id));
    }
}