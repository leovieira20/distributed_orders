using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common.Messaging.RabbitMq
{
    public class Producer : ISystemBus
    {
        private readonly ILogger<Producer> _logger;
        private readonly IModel _channel;

        public Producer(
            ILogger<Producer> logger,
            IOptions<RabbitMqConfiguration> options)
        {
            _logger = logger;
            var config = options.Value;
            var factory = new ConnectionFactory { HostName = config.Host };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Post<T>(T e) where T : IEvent
        {
            var exchangeName = typeof(T).GetProperty(nameof(IEvent.Name))?.GetValue(e)?.ToString();
            
            _channel.ExchangeDeclare(
                exchange: exchangeName,
                type: ExchangeType.Fanout, 
                durable: true);
                
            var body = Encoding.UTF8.GetBytes((string) JsonConvert.SerializeObject(e));

            try
            {
                _channel.BasicPublish(
                    exchange: exchangeName,
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error trying to publish message");
            }
        }
    }
}