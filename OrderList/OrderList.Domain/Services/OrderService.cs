using System.Collections.Generic;
using System.Threading.Tasks;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Services
{
    public interface IOrderService
    {
        Task<Order> GetAsync(string id);
        Task<IEnumerable<Order>> GetAsync(int page, int size);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Order> GetAsync(string id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAsync(int page, int size)
        {
            return await _repository.GetAsync(page, size);
        }
    }
}