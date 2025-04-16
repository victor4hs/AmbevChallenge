using Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.UpdateSaleItem;

/// <summary>
/// Profile for mapping between Application and API UpdateSaleItem responses.
/// </summary>
public class UpdateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the UpdateSaleItem feature.
    /// </summary>
    public UpdateSaleItemProfile()
    {
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
    }
}