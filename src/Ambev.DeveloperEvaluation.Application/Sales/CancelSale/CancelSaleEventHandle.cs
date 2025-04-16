using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleEventHandler : INotificationHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleCancelledEvent> _logger;

    public CancelSaleEventHandler(ILogger<SaleCancelledEvent> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Sale canceled with success: {notification.Sale?.Id}");
    }
}
