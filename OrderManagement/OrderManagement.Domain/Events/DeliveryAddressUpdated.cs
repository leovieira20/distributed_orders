using Common.Messaging.RabbitMq;

namespace OrderManagement.Domain.Events
{
    public class DeliveryAddressUpdated : IEvent
    {
        public static string Name => nameof(DeliveryAddressUpdated);
    }
}