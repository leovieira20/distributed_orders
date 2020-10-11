using System;
using Common.Messaging.RabbitMq;
using OrderList.Domain.Events.Inbound;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Consumers
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        private readonly IOrderRepository _repository;

        public OrderCancelledConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public void Consume(OrderCancelled message)
        {
            try
            {
                _repository.Delete(message.Order.OrderId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}