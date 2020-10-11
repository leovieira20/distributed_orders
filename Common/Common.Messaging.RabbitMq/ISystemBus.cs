namespace Common.Messaging.RabbitMq
{
    public interface ISystemBus
    {
        void Post<T>(T e) where T: IEvent;
    }
}