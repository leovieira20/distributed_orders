using Common.Messaging.RabbitMq;
using OrderList.Domain.Models;

namespace OrderList.Domain.Events.Inbound
{
    public class DeliveryAddressUpdated : IEvent
    {
        public static string Name => nameof(DeliveryAddressUpdated);
        public string OrderId { get; set; }
        public Address NewAddress { get; set; }
    }
}