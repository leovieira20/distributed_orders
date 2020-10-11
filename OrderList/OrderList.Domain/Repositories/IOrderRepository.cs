using System.Collections.Generic;
using System.Threading.Tasks;
using OrderList.Domain.Models;

namespace OrderList.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(string id);
        Task<List<Order>> GetAsync(int page, int size);
        Task CreateAsync(Order order);
        Task Delete(string orderId);
        Task UpdateStatusAsync(Order order);
        Task UpdateDeliveryAddress(Order order, Address address);
    }
}