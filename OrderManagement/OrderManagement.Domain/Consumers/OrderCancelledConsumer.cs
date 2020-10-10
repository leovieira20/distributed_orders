using System;
using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Consumers
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        private readonly IOrderRepository _repository;

        public OrderCancelledConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async void Consume(OrderCancelled message)
        {
            try
            {
                await _repository.CancelOrderAsync(message.OrderId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}