using System;
using Common.Messaging.RabbitMq;
using OrderList.Domain.Events.Inbound;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IOrderRepository _repository;

        public OrderCreatedConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async void Consume(OrderCreated message)
        {
            try
            {
                await _repository.CreateAsync(message.Order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}