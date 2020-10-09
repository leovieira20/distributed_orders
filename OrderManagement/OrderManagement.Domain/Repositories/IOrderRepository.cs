using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CancelOrderAsync(Guid id);
        Task UpdateDeliveryAddress(Guid id, Address newAddress);
        Task UpdateOrderItems(Guid id, IEnumerable<OrderItem> items);
        Task<Product> GetProduct(Guid id);
    }
}