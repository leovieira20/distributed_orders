using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;
using OrderManagement.Repository.Mongo.Models;

namespace OrderList.Repository.Mongo
{
    public class MongoOrderRepository : IOrderRepository
    {
        private readonly string _dbName = "distributed_orders";
        private readonly ILogger<MongoOrderRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<OrderDTO> _collection;

        public MongoOrderRepository(
            ILogger<MongoOrderRepository> logger, 
            IOptions<MongoConfiguration> options,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            var config = options.Value;
            var client = new MongoClient($"mongodb://{config.Host}:{config.Port}/{_dbName}");
            _collection = client
                .GetDatabase(_dbName)
                .GetCollection<OrderDTO>("orders");
        }
        public async Task<Order> GetAsync(string id)
        {
            try
            {
                var items = await _collection
                    .FindAsync(Builders<OrderDTO>.Filter.Where(x => x.OrderId == id));

                return _mapper.Map<Order>(items.FirstOrDefault());
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
                var list = await _collection
                    .Find(Builders<OrderDTO>.Filter.Empty)
                    .Skip(page * size)
                    .Limit(size)
                    .ToListAsync();
                return _mapper.Map<List<Order>>(list);
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
                await _collection.InsertOneAsync(_mapper.Map<OrderDTO>(order));
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
                    .DeleteOneAsync(Builders<OrderDTO>.Filter.Eq(order => order.OrderId, orderId));
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
                var filter = Builders<OrderDTO>.Filter.Eq(x => x.OrderId, order.OrderId);
                var update = Builders<OrderDTO>.Update.Set(x => x.Status, _mapper.Map<OrderStatusDTO>(order.Status));
                await _collection.FindOneAndUpdateAsync(filter, update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.FindOneAndUpdateAsync)}");
                throw;
            }
        }

        public async Task UpdateDeliveryAddress(Order order, Address address)
        {
            try
            {
                var filter = Builders<OrderDTO>.Filter.Eq(x => x.OrderId, order.OrderId);
                var update = Builders<OrderDTO>.Update.Set(x => x.DeliveryAddress, _mapper.Map<AddressDTO>(order.DeliveryAddress));
                await _collection.FindOneAndUpdateAsync(filter, update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.FindOneAndUpdateAsync)}");
                throw;
            }
        }
    }
}