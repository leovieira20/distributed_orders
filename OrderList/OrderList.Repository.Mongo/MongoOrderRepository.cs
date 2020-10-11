using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Repository.Mongo
{
    public class MongoOrderRepository : IOrderRepository
    {
        private readonly string _dbName = "distributed_orders";
        private readonly ILogger<MongoOrderRepository> _logger;
        private readonly IMongoCollection<Order> _collection;

        public MongoOrderRepository(
            ILogger<MongoOrderRepository> logger, 
            IOptions<MongoConfiguration> options)
        {
            _logger = logger;

            var config = options.Value;
            var client = new MongoClient($"mongodb://{config.Host}:{config.Port}/{_dbName}");
            _collection = client
                .GetDatabase(_dbName)
                .GetCollection<Order>("orders");
        }
        public async Task<Order> GetAsync(string id)
        {
            try
            {
                var items = await _collection
                    .FindAsync(Builders<Order>.Filter.Where(x => x.OrderId == id));

                return items.FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.FindAsync)}");
                throw;
            }
        }

        public async Task<List<Order>> GetAsync(int page, int size)
        {
            try
            {
                return await _collection
                    .Find(Builders<Order>.Filter.Empty)
                    .Skip(page * size)
                    .Limit(size)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.FindAsync)}");
                throw;
            }
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await _collection.InsertOneAsync(order);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.InsertOneAsync)}");
                throw;
            }
        }

        public async Task Delete(string orderId)
        {
            try
            {
                await _collection
                    .DeleteOneAsync(Builders<Order>.Filter.Eq(order => order.OrderId, orderId));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.DeleteOneAsync)}");
                throw;
            }
        }

        public async Task UpdateStatusAsync(Order order)
        {
            try
            {
                var filter = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
                var update = Builders<Order>.Update.Set(x => x.Status, order.Status);
                await _collection.FindOneAndUpdateAsync(filter, update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.FindOneAndUpdateAsync)}");
                throw;
            }
        }

        public async Task UpdateDeliveryAddress(Order order, Address address)
        {
            try
            {
                var filter = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
                var update = Builders<Order>.Update.Set(x => x.DeliveryAddress, order.DeliveryAddress);
                await _collection.FindOneAndUpdateAsync(filter, update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.FindOneAndUpdateAsync)}");
                throw;
            }
        }
    }
}