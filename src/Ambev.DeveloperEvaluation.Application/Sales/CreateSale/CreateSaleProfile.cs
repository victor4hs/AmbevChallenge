using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between entities and objects related to the CreateSale operation.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CreateSale operation.
    /// </summary>
    public CreateSaleProfile()
    {
        // Maps the CreateSaleCommand to the Sale entity.
        CreateMap<CreateSaleCommand, Sale>();

        // Maps the Sale entity to the CreateSaleResult object.
        CreateMap<Sale, CreateSaleResult>();
    }
}