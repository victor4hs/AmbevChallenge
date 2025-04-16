using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class ItemCancelledEvent : INotification
{
    public SaleItem SaleItem { get; set; }

    public ItemCancelledEvent(SaleItem saleItem)
    {
        SaleItem = saleItem;
    }
}