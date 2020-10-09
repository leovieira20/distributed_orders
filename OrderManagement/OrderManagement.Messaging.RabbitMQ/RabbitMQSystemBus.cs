using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrderManagement.Domain.Events;
using OrderManagement.Domain.Messaging;
using RabbitMQ.Client;

namespace OrderManagement.Messaging.RabbitMQ
{
    public class RabbitMQSystemBus : ISystemBus
    {
        private readonly ConnectionFactory _factory;
        private const string EventName = "orderCreated";

        public RabbitMQSystemBus()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
        }

        public Task PostAsync(IEvent message)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.ExchangeDeclare(
                exchange: EventName, 
                type: ExchangeType.Fanout, 
                durable: true);
                
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish(
                exchange: EventName,
                routingKey: "",
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }
    }
}