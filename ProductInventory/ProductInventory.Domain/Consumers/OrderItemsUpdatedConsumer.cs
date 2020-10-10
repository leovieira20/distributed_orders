using System.Collections.Generic;
using Common.Messaging.RabbitMq;
using ProductInventory.Domain.Events.Inbound;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;

namespace ProductInventory.Domain.Consumers
{
    public class OrderItemsUpdatedConsumer : IConsumer<OrderItemsUpdated>
    {
        private readonly IProductRepository _repository;

        public OrderItemsUpdatedConsumer(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public async void Consume(OrderItemsUpdated message)
        {
            var productsToUpdate = new List<Product>();
            foreach (var i in message.OldItems)
            {
                var p = await _repository.Get(i.ProductId);
                p.ReleaseQuantity(i.Quantity);
                productsToUpdate.Add(p);
            }
            
            await _repository.BulkUpdate(productsToUpdate);
            productsToUpdate.Clear();

            foreach (var i in message.NewItems)
            {
                var p = await _repository.Get(i.ProductId);
                p.ReserveQuantity(i.Quantity);
                productsToUpdate.Add(p);
            }

            await _repository.BulkUpdate(productsToUpdate);
        }
    }
}