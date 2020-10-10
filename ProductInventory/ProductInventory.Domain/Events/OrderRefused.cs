using Common.Messaging.RabbitMq;

namespace ProductInventory.Domain.Events
{
    public class OrderRefused : IEvent
    {
        public static string Name => nameof(OrderRefused);
        public string OrderId { get; set; }
    }
}