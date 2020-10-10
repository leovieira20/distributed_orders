using Common.Messaging.RabbitMq;

namespace OrderManagement.Domain.Events.Outbound
{
    public class DeliveryAddressUpdated : IEvent
    {
        public static string Name => nameof(DeliveryAddressUpdated);
    }
}