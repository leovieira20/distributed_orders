using System;
using Common.Messaging.RabbitMq;
using OrderList.Domain.Events.Inbound;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Consumers
{
    public class OrderRefusedConsumer : IConsumer<OrderRefused>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRefusedConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        public void Consume(OrderRefused message)
        {
            try
            {
                var order = _orderRepository.Get(message.OrderId);
                order.Status = OrderStatus.Refused;
                _orderRepository.Update(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}