using Common.Messaging.RabbitMq;

namespace ProductInventory.Domain.Events
{
    public class OrderConfirmed : IEvent
    {
        public static string Name => nameof(OrderConfirmed);
        public string OrderId { get; set; }
    }
}