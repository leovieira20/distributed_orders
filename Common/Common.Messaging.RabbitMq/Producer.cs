using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common.Messaging.RabbitMq
{
    public class Producer : ISystemBus
    {
        private readonly IModel _channel;

        public Producer()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public Task PostAsync<T>(T e) where T : IEvent
        {
            var exchangeName = typeof(T).GetProperty(nameof(IEvent.Name))?.GetValue(e)?.ToString();
            
            _channel.ExchangeDeclare(
                exchange: exchangeName,
                type: ExchangeType.Fanout, 
                durable: true);
                
            var body = Encoding.UTF8.GetBytes((string) JsonConvert.SerializeObject(e));
            _channel.BasicPublish(
                exchange: exchangeName,
                routingKey: "",
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }
    }
}