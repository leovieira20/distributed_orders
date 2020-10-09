using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Repository.Redis
{
    public class RedisOrderRepository : IOrderRepository
    {
        public Task<Order> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAsync(int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}