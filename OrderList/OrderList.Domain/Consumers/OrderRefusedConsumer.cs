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
        
        public async void Consume(OrderRefused message)
        {
            try
            {
                var order = await _orderRepository.GetAsync(message.OrderId);
                order.Status = OrderStatus.Refused;
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