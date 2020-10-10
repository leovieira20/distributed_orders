using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;
using ServiceStack.Redis;

namespace OrderList.Repository.Redis
{
    public class RedisOrderRepository : IOrderRepository
    {
        private readonly IRedisClient _client;

        public RedisOrderRepository()
        {
            var manager = new RedisManagerPool("localhost:6379");
            _client = manager.GetClient();
        }

        public Task<Order> GetAsync(string id)
        {
            return Task.FromResult(_client.As<Order>().GetById(id));
        }

        public Task<IEnumerable<Order>> GetAsync(int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Order order)
        {
            _client.As<Order>()
                .Store(order);
            return Task.CompletedTask;
        }
    }
}