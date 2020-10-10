using System;
using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Consumers
{
    public class OrderRefusedConsumer : IConsumer<OrderRefused>
    {
        private readonly IOrderRepository _repository;

        public OrderRefusedConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async void Consume(OrderRefused message)
        {
            try
            {
                await _repository.RefuseOrderAsync(message.OrderId);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}