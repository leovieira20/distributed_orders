using System;
using Newtonsoft.Json.Linq;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Consumers
{
    public interface IOrderCreatedConsumer
    {
        void Create(JObject message);
    }

    public class OrderCreatedConsumer : IOrderCreatedConsumer
    {
        private readonly IOrderRepository _repository;

        public OrderCreatedConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public void Create(JObject message)
        {
            try
            {
                var order = message.Value<JObject>("Order").ToObject<Order>();
                _repository.CreateAsync(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}