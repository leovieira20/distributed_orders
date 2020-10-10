using Common.Messaging.RabbitMq;
using ProductInventory.Domain.Model;

namespace ProductInventory.Domain.Events
{
    public class OrderCreated : IEvent
    {
        public static string Name => nameof(OrderCreated);
        public Order Order { get; set; }
    }
}