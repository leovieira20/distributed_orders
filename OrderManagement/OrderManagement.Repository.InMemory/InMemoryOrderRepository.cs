using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Repository.InMemory
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        public Task CreateAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task CancelOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDeliveryAddress(Guid id, Address newAddress)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderItems(Guid id, IEnumerable<OrderItem> items)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}