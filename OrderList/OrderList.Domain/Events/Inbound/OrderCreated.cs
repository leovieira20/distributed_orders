using Common.Messaging.RabbitMq;
using OrderList.Domain.Models;

namespace OrderList.Domain.Events.Inbound
{
    public class OrderCreated : IEvent
    {
        public static string Name => nameof(OrderCreated);
        public Order Order { get; set; }
    }
}