using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleItemEventHandler : INotificationHandler<ItemCancelledEvent>
{
    private readonly ILogger<CancelSaleItemEventHandler> _logger;

    public CancelSaleItemEventHandler(ILogger<CancelSaleItemEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Item canceled with success: {notification.SaleItem?.Id}");
    }
}
