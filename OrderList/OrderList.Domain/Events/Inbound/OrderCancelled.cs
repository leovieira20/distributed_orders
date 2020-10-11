using Common.Messaging.RabbitMq;
using OrderList.Domain.Models;

namespace OrderList.Domain.Events.Inbound
{
    public class OrderCancelled : IEvent
    {
        public static string Name => nameof(OrderCancelled);
        public Order Order { get; set; }
    }
}