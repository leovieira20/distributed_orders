using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Events.Inbound
{
    public class OrderCancelled : IEvent
    {
        public static string Name => nameof(OrderCancelled);
        public Order Order { get; set; }
    }
}