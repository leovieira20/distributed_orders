using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Repository.Mongo
{
    public class MongoOrderRepository : IOrderRepository
    {
        private readonly string dbName = "distributed_orders";
        private readonly MongoClient _client;
        private readonly IMongoCollection<Order> _collection;

        public MongoOrderRepository()
        {
            _client = new MongoClient($"mongodb://localhost:27017/{dbName}");
            _collection = _client
                .GetDatabase(dbName)
                .GetCollection<Order>("orders");
        }    
        
        public async Task CreateAsync(Order order)
        {
            await _collection
                .InsertOneAsync(order);
        }

        public async Task<Order> CancelOrderAsync(string id)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<Order>(p => p.OrderId == id);
                var update = new UpdateDefinitionBuilder<Order>().Set(p => p.Status, OrderStatus.Cancelled);

                return await _collection.FindOneAndUpdateAsync<Order>(filter, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateDeliveryAddressAsync(string id, Address newAddress)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<Order>(p => p.OrderId == id);
                var update = new UpdateDefinitionBuilder<Order>().Set(p => p.DeliveryAddress, newAddress);

                await _collection.FindOneAndUpdateAsync<Order>(filter, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task UpdateOrderItemsAsync(string id, IEnumerable<OrderItem> items)
        {
            throw new NotImplementedException();
        }
        
        public async Task ConfirmOrderAsync(string orderId)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<Order>(p => p.OrderId == orderId);
                var update = new UpdateDefinitionBuilder<Order>().Set(p => p.Status, OrderStatus.Confirmed);

                await _collection.FindOneAndUpdateAsync<Order>(filter, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task RefuseOrderAsync(string orderId)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<Order>(p => p.OrderId == orderId);
                var update = new UpdateDefinitionBuilder<Order>().Set(p => p.Status, OrderStatus.Refused);

                await _collection.FindOneAndUpdateAsync<Order>(filter, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}