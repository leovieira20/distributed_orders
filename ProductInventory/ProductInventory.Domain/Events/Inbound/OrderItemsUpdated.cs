using System.Collections.Generic;
using Common.Messaging.RabbitMq;
using ProductInventory.Domain.Model;

namespace ProductInventory.Domain.Events.Inbound
{
    public class OrderItemsUpdated : IEvent
    {
        public static string Name => nameof(OrderItemsUpdated);
        public IEnumerable<OrderItem> OldItems { get; set; }
        public IEnumerable<OrderItem> NewItems { get; set; }
    }
}