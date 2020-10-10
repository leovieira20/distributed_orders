using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CancelOrderAsync(string id);
        Task UpdateDeliveryAddress(string id, Address newAddress);
        Task UpdateOrderItems(string id, IEnumerable<OrderItem> items);
        Task<Product> GetProduct(string id);
    }
}