using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Profile for mapping between Sale entity and CancelSaleResult
/// </summary>
public class SaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CancelSale operation
    /// </summary>
    public SaleProfile()
    {
        CreateMap<Sale, SaleResult>();
        CreateMap<SaleItem, SaleItemResult>();
        CreateMap<GetAllSalesQuery, SaleQueryFilter>();
        
    }
}
