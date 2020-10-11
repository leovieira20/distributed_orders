using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
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

        public MongoOrderRepository(IOptions<MongoConfiguration> options)
        {
            var config = options.Value;
            _client = new MongoClient($"mongodb://{config.Host}:{config.Port}/{dbName}");
            _collection = _client
                .GetDatabase(dbName)
                .GetCollection<Order>("orders");
        }

        public async Task<Order> GetAsync(string id)
        {
            try
            {
                var result = await _collection.FindAsync(o => o.OrderId == id);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await _collection
                    .InsertOneAsync(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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

        public async Task<Order> UpdateOrderItemsAsync(string id, IEnumerable<OrderItem> items)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<Order>(p => p.OrderId == id);
                var update = new UpdateDefinitionBuilder<Order>().Set(p => p.Items, items);

                return await _collection.FindOneAndUpdateAsync<Order>(filter, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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