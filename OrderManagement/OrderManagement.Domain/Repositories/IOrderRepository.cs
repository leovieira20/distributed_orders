using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Domain.Model;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(string id);
        Task CreateAsync(Order order);
        Task<Order> CancelOrderAsync(string id);
        Task UpdateDeliveryAddressAsync(string id, Address newAddress);
        Task<Order> UpdateOrderItemsAsync(string id, IEnumerable<OrderItem> items);
        Task ConfirmOrderAsync(string orderId);
        Task RefuseOrderAsync(string orderId);
    }
}