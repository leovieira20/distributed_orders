using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Events
{
    public class OrderCreated : IEvent
    {
        public Order Order { get; set; }
    }
}