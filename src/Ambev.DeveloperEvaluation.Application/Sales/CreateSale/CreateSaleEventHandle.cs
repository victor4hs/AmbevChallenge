using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleEventHandler : INotificationHandler<SaleCreatedEvent>
{
    private readonly ILogger<CreateSaleEventHandler> _logger;

    public CreateSaleEventHandler(ILogger<CreateSaleEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Created sale for success: {notification.Sale}");
    }
}
