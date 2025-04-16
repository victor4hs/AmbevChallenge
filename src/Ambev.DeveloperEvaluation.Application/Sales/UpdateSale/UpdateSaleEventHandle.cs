using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleEventHandler : INotificationHandler<SaleModifiedEvent>
{
    private readonly ILogger<UpdateSaleEventHandler> _logger;

    public UpdateSaleEventHandler(ILogger<UpdateSaleEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updated sale with success: {notification.Sale}");
    }
}

