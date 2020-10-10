using Common.Messaging.RabbitMq;

namespace OrderManagement.Domain.Events.Outbound
{
    public class OrderItemsUpdated : IEvent
    {
        public static string Name => nameof(OrderCreated);
    }
}