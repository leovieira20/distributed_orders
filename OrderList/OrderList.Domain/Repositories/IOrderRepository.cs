using System.Collections.Generic;
using OrderList.Domain.Models;

namespace OrderList.Domain.Repositories
{
    public interface IOrderRepository
    {
        Order Get(string id);
        List<Order> Get(int page, int size);
        void Create(Order order);
        void Delete(string orderId);
        void Update(Order order);
    }
}