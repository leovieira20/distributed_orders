using System.Collections.Generic;
using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Events.Outbound
{
    public class OrderItemsUpdated : IEvent
    {
        public static string Name => nameof(OrderCreated);
        public IEnumerable<OrderItem> OldItems { get; set; }
        public IEnumerable<OrderItem> NewItems { get; set; }
    }
}