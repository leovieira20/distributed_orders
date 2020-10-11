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
        
        public async void Consume(OrderConfirmed message)
        {
            try
            {
                var order = await _orderRepository.GetAsync(message.OrderId);
                order.Status = OrderStatus.Confirmed;
                await _orderRepository.UpdateStatusAsync(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}