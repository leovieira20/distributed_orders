using System;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.Messaging.RabbitMq
{
    public class ConsumerWrapper<T> where T : IEvent
    {
        private readonly ILogger<ConsumerWrapper<T>> _logger;
        private readonly IConsumer<T> _consumer;
        private readonly IModel _channel;

        public ConsumerWrapper(
            ILogger<ConsumerWrapper<T>> logger,
            IOptions<RabbitMqConfiguration> options,
            IConsumer<T> consumer)
        {
            _logger = logger;
            _consumer = consumer;
            var config = options.Value;
            var factory = new ConnectionFactory { HostName = config.Host };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Consume()
        {
            var exchangeName = typeof(T).GetProperty(nameof(IEvent.Name), BindingFlags.Public | BindingFlags.Static)
                ?.GetValue(null)?.ToString();

            _channel.ExchangeDeclare(
                exchange: exchangeName,
                type: ExchangeType.Fanout, 
                durable: true);
            
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
                try
                {
                    _consumer.Consume(JsonConvert.DeserializeObject<T>(message));
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error trying to execute consumer");
                }
            };

            _channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}