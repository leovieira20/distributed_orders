using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderList.Domain.Models;

namespace OrderList.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(Guid id);
        Task<IEnumerable<Order>> GetAsync(int page, int size);
        Task CreateAsync(Order order);
    }
}