using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _repository;
        private readonly ISystemBus _bus;

        public OrderService(
            ILogger<OrderService> logger,
            IOrderRepository repository, 
            ISystemBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await _repository.CreateAsync(order);
                await _bus.PostAsync(new OrderCreated
                {
                    Order = order
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to create order");
                throw;
            }
        }

        public async Task UpdateDeliveryAddressAsync(string id, Address newAddress)
        {
            try
            {
                await _repository.UpdateDeliveryAddressAsync(id, newAddress);
                await _bus.PostAsync(new DeliveryAddressUpdated { OrderId = id, NewAddress = newAddress });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to update delivery address");
                throw;
            }
        }
        
        public async Task UpdateOrderItemsAsync(string id, IEnumerable<OrderItem> items)
        {
            try
            {
                var existingOrder = await _repository.GetAsync(id);
                await _repository.UpdateOrderItemsAsync(id, items);
                var updatedOrder = await _repository.GetAsync(id);
                await _bus.PostAsync(new OrderItemsUpdated { NewItems = updatedOrder.Items, OldItems = existingOrder.Items });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to update order items");
                throw;
            }
        }

        public async Task CancelOrderAsync(string id)
        {
            try
            {
                var order = await _repository.CancelOrderAsync(id);
                await _bus.PostAsync(new OrderCancelled { Order = order });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to cancel order");
                throw;
            }
        }
    }
}