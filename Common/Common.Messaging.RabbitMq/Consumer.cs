using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.Messaging.RabbitMq
{
    public class Consumer<T> where T : IEvent
    {
        private readonly Action<T> _handler;
        private readonly IModel _channel;

        public Consumer(Action<T> handler)
        {
            _handler = handler;
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Consume()
        {
            var exchangeName = typeof(T).GetProperty(nameof(IEvent.Name), BindingFlags.Public | BindingFlags.Static)?.GetValue(null)?.ToString();
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _handler(JsonConvert.DeserializeObject<T>(message));
            };
            
            _channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}