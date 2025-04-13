using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Profile for mapping CancelSale feature requests to commands.
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the CancelSale feature.
    /// </summary>
    public CancelSaleProfile()
    {
        // Maps a GUID to the CancelSaleCommand, constructing the command with the provided ID.
        CreateMap<Guid, Application.Sales.CancelSale.CancelSaleCommand>()
            .ConstructUsing(id => new Application.Sales.CancelSale.CancelSaleCommand(id));
    }
}