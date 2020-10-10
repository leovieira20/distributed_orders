using System;
using Common.Messaging.RabbitMq;
using ProductInventory.Domain.Events;
using ProductInventory.Domain.Repository;

namespace ProductInventory.Domain.Consumers
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        private readonly IProductRepository _repository;

        public OrderCancelledConsumer(IProductRepository repository)
        {
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
                Console.WriteLine(e);
                throw;
            }
        }
    }
}