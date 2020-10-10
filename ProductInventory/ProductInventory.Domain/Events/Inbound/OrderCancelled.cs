using Common.Messaging.RabbitMq;
using ProductInventory.Domain.Model;

namespace ProductInventory.Domain.Events.Inbound
{
    public class OrderCancelled : IEvent
    {
        public static string Name => nameof(OrderCancelled);
        public Order Order { get; set; }
    }
}