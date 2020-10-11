using Common.Messaging.RabbitMq;

namespace OrderList.Domain.Events.Inbound
{
    public class OrderConfirmed : IEvent
    {
        public static string Name => nameof(OrderConfirmed);
        public string OrderId { get; set; }
    }
}