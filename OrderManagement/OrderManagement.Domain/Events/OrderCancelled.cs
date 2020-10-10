using Common.Messaging.RabbitMq;

namespace OrderManagement.Domain.Events
{
    public class OrderCancelled : IEvent
    {
        public static string Name => nameof(OrderCancelled);
    }
}