using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Events.Outbound
{
    public class DeliveryAddressUpdated : IEvent
    {
        public static string Name => nameof(DeliveryAddressUpdated);
        public string OrderId { get; set; }
        public Address NewAddress { get; set; }
    }
}