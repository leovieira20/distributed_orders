using System;
using Common.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
using ProductInventory.Domain.Events;
using ProductInventory.Domain.Events.Inbound;
using ProductInventory.Domain.Repository;

namespace ProductInventory.Domain.Consumers
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        private readonly ILogger<OrderCancelledConsumer> _logger;
        private readonly IProductRepository _repository;

        public OrderCancelledConsumer(
            ILogger<OrderCancelledConsumer> logger,
            IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public async void Consume(OrderCancelled message)
        {
            try
            {
                foreach (var i in message.Order.Items)
                {
                    var product = await _repository.Get(i.ProductId);
                    product.ReleaseQuantity(i.Quantity);
                    await _repository.Update(product);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to cancel order");
                throw;
            }
        }
    }
}