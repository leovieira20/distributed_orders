using System;
using System.Collections.Generic;
using Common.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
using ProductInventory.Domain.Events.Inbound;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;

namespace ProductInventory.Domain.Consumers
{
    public class OrderItemsUpdatedConsumer : IConsumer<OrderItemsUpdated>
    {
        private readonly ILogger<OrderItemsUpdatedConsumer> _logger;
        private readonly IProductRepository _repository;

        public OrderItemsUpdatedConsumer(
            ILogger<OrderItemsUpdatedConsumer> logger,
            IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public async void Consume(OrderItemsUpdated message)
        {
            try
            {
                var productsToUpdate = new List<Product>();
                foreach (var i in message.OldItems)
                {
                    var p = await _repository.GetAsync(i.ProductId);
                    p.ReleaseQuantity(i.Quantity);
                    productsToUpdate.Add(p);
                }
            
                await _repository.BulkUpdate(productsToUpdate);
                productsToUpdate.Clear();

                foreach (var i in message.NewItems)
                {
                    var p = await _repository.GetAsync(i.ProductId);
                    p.ReserveQuantity(i.Quantity);
                    productsToUpdate.Add(p);
                }

                await _repository.BulkUpdate(productsToUpdate);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to update order items");
                throw;
            }
        }
    }
}