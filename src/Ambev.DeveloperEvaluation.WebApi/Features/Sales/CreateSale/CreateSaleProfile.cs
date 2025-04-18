using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesItem.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Common;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.SalesItem.Common;

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
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<SaleResult, SaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
        CreateMap<CreateSaleItemResult, SaleItemResponse>()
            .ForMember(
                dest => dest.Product, 
                opt => opt.MapFrom(
                    src => new Application.SalesItem.Common.ProductInfo
                    {
                        Id = src.ProductId,
                        Name = src.ProductName,
                        UnitPrice = src.UnitPrice
                    }
                )
            );
    }
}