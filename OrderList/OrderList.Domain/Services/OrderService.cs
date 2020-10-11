using System.Collections.Generic;
using System.Threading.Tasks;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Services
{
    public interface IOrderService
    {
        Order Get(string id);
        List<Order> Get(int page, int size);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public Order Get(string id)
        {
            return _repository.Get(id);
        }

        public List<Order> Get(int page, int size)
        {
            return _repository.Get(page, size);
        }
    }
}