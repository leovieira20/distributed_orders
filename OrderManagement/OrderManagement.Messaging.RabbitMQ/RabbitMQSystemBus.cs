using System.Threading.Tasks;
using OrderManagement.Domain.Events;
using OrderManagement.Domain.Messaging;

namespace OrderManagement.Messaging.RabbitMQ
{
    public class RabbitMQSystemBus : ISystemBus
    {
        public Task PostAsync(IEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}