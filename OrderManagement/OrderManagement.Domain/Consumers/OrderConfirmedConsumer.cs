using System;
using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Consumers
{
    public class OrderConfirmedConsumer : IConsumer<OrderConfirmed>
    {
        private readonly IOrderRepository _repository;

        public OrderConfirmedConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async void Consume(OrderConfirmed message)
        {
            try
            {
                await _repository.ConfirmOrderAsync(message.OrderId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}