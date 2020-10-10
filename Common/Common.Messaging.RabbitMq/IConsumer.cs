namespace Common.Messaging.RabbitMq
{
    public interface IConsumer<T> where T : IEvent
    {
        public void Consume(T message);
    }
}