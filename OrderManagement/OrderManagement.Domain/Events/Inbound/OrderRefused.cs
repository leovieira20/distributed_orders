using Common.Messaging.RabbitMq;

namespace OrderManagement.Domain.Events.Inbound
{
    public class OrderRefused : IEvent
    {
        public static string Name => nameof(OrderRefused);
        public string OrderId { get; set; }
    }
}