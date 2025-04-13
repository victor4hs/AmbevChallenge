using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Profile for mapping between entities and objects related to the GetSale operation.
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the GetSale operation.
    /// </summary>
    public GetSaleProfile()
    {
        // Maps the Sale entity to the SaleResult object.
        CreateMap<Sale, SaleResult>();
    }
}