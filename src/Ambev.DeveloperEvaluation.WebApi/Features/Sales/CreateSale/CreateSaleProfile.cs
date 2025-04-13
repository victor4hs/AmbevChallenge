using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSales responses.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CreateSales feature.
    /// </summary>
    public CreateSaleProfile()
    {
        // Maps the CreateSaleRequest to the CreateSaleCommand.
        CreateMap<CreateSaleRequest, CreateSaleCommand>();

        // Maps the CreateSaleResult to the SaleResponse.
        CreateMap<CreateSaleResult, SaleResponse>();
    }
}