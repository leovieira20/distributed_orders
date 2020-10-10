using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Events
{
    public class OrderCreated : IEvent
    {
        public static string Name => nameof(OrderCreated);
        public Order Order { get; set; }
    }
}