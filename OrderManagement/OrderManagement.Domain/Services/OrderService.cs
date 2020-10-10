using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Messaging.RabbitMq;
using OrderManagement.Domain.Events;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Events.Outbound;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Services
{
    public interface IOrderService
    {
        Task CreateAsync(Order order);
        Task UpdateDeliveryAddressAsync(string id, Address newAddress);
        Task UpdateOrderItemsAsync(string id, IEnumerable<OrderItem> items);
        Task CancelOrderAsync(string id);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ISystemBus _bus;

        public OrderService(IOrderRepository repository, ISystemBus bus)
        {
            _repository = repository;
            _bus = bus;
        }

        public async Task CreateAsync(Order order)
        {
            await _repository.CreateAsync(order);
            await _bus.PostAsync(new OrderCreated
            {
                Order = order
            });
        }

        public async Task UpdateDeliveryAddressAsync(string id, Address newAddress)
        {
            await _repository.UpdateDeliveryAddressAsync(id, newAddress);
            await _bus.PostAsync(new DeliveryAddressUpdated());
        }
        
        public async Task UpdateOrderItemsAsync(string id, IEnumerable<OrderItem> items)
        {
            await _repository.UpdateOrderItemsAsync(id, items);
            await _bus.PostAsync(new OrderItemsUpdated());
        }

        public async Task CancelOrderAsync(string id)
        {
            await _repository.CancelOrderAsync(id);
            await _bus.PostAsync(new OrderCancelled());
        }

        public Task FulfillOrderAsync()
        {
            throw new NotImplementedException();
        }
    }
}