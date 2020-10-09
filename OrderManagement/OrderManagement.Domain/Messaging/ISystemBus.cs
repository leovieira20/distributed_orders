using System.Threading.Tasks;
using OrderManagement.Domain.Events;
using OrderManagement.Domain.Services;

namespace OrderManagement.Domain.Messaging
{
    public interface ISystemBus
    {
        Task PostAsync(IEvent message);
    }
}