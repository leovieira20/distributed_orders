using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Domain.Events;
using OrderManagement.Domain.Messaging;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Services
{
    public interface IOrderService
    {
        Task CreateAsync(Order order);
        Task UpdateDeliveryAddressAsync(Guid id, Address newAddress);
        Task UpdateOrderItemsAsync(Guid id, IEnumerable<OrderItem> items);
        Task CancelOrderAsync(Guid id);
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
            await _bus.PostAsync(new OrderCreated());
        }

        public async Task UpdateDeliveryAddressAsync(Guid id, Address newAddress)
        {
            await _repository.UpdateDeliveryAddress(id, newAddress);
            await _bus.PostAsync(new DeliveryAddressUpdated());
        }
        
        public async Task UpdateOrderItemsAsync(Guid id, IEnumerable<OrderItem> items)
        {
            await _repository.UpdateOrderItems(id, items);
            await _bus.PostAsync(new OrderItemsUpdated());
        }

        public async Task CancelOrderAsync(Guid id)
        {
            await _repository.CancelOrderAsync(id);
            await _bus.PostAsync(new OrderCancelled());
        }
    }
}