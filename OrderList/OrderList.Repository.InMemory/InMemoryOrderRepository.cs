using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Repository.InMemory
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private static IList<Order> _repository = new List<Order>(); 
        
        public Task<Order> GetAsync(Guid id)
        {
            return Task.FromResult(_repository.SingleOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<Order>> GetAsync(int page, int size)
        {
            return Task.FromResult(_repository
                .Skip(page * size)
                .Take(size));
        }

        public Task CreateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}