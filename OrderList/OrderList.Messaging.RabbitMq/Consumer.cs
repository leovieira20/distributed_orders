using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderList.Messaging.RabbitMq
{
    public class Consumer<T>
    {
        private readonly Action<T> _handler;
        private const string ExchangeName = "orderCreated";
        private readonly ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public Consumer(Action<T> handler)
        {
            _handler = handler;
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Consume()
        {
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(
                queue: queueName,
                exchange: ExchangeName,
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