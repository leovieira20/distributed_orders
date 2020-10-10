using System;
using OrderList.Domain.Events;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Consumers
{
    public interface IOrderCreatedConsumer
    {
        void Create(OrderCreated message);
    }

    public class OrderCreatedConsumer : IOrderCreatedConsumer
    {
        private readonly IOrderRepository _repository;

        public OrderCreatedConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public void Create(OrderCreated message)
        {
            try
            {
                _repository.CreateAsync(message.Order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}