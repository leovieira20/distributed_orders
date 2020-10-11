using System;
using Common.Messaging.RabbitMq;
using OrderList.Domain.Events.Inbound;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Consumers
{
    public class OrderConfirmedConsumer : IConsumer<OrderConfirmed>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderConfirmedConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        public void Consume(OrderConfirmed message)
        {
            try
            {
                var order = _orderRepository.Get(message.OrderId);
                order.Status = OrderStatus.Confirmed;
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