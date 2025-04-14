using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Profile for mapping between Application and API UpdateSales responses.
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the UpdateSales feature.
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
    }
}