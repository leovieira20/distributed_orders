using System.Threading.Tasks;

namespace Common.Messaging.RabbitMq
{
    public interface ISystemBus
    {
        Task PostAsync<T>(T e) where T: IEvent;
    }
}