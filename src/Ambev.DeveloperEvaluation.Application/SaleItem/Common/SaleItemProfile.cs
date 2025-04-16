using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.SalesItem.UpdateSaleItem;

namespace Ambev.DeveloperEvaluation.Application.SalesItem.Common;

/// <summary>
/// Profile for mapping between SaleItem entity and CancelSaleItemResult
/// </summary>
public class SaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CancelSaleItem operation
    /// </summary>
    public SaleItemProfile()
    {
        CreateMap<SaleItem, SaleItemResult>();
        CreateMap<UpdateSaleItemCommand, SaleItem>();
    }
}
