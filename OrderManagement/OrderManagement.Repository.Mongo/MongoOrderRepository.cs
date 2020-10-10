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

        public Task CancelOrderAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDeliveryAddress(string id, Address newAddress)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderItems(string id, IEnumerable<OrderItem> items)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(string id)
        {
            throw new NotImplementedException();
        }
    }
}