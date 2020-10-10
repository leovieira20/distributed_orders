using Common.Messaging.RabbitMq;

namespace OrderManagement.Domain.Events.Inbound
{
    public class OrderCancelled : IEvent
    {
        public static string Name => nameof(OrderCancelled);
        public string OrderId { get; set; }
    }
}