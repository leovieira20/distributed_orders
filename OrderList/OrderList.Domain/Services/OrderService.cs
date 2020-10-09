using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Services
{
    public interface IOrderService
    {
        Task<Order> GetAsync(Guid id);
        Task<IEnumerable<Order>> GetAsync(int page, int size);
        void Create(JObject message);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Order> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAsync(int page, int size)
        {
            return await _repository.GetAsync(page, size);
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