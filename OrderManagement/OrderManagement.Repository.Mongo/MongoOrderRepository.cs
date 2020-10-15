using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderManagement.Domain.Model;
using OrderManagement.Domain.Repositories;
using OrderManagement.Repository.Mongo.Models;

namespace OrderManagement.Repository.Mongo
{
    public class MongoOrderRepository : IOrderRepository
    {
        private readonly ILogger<MongoOrderRepository> _logger;
        private readonly IMapper _mapper;
        private readonly string dbName = "distributed_orders";
        private readonly MongoClient _client;
        private readonly IMongoCollection<OrderDTO> _collection;

        public MongoOrderRepository(
            ILogger<MongoOrderRepository> logger,
            IOptions<MongoConfiguration> options,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            var config = options.Value;
            _client = new MongoClient($"mongodb://{config.Host}:{config.Port}/{dbName}");
            _collection = _client
                .GetDatabase(dbName)
                .GetCollection<OrderDTO>("orders");
        }

        public async Task<Order> GetAsync(string id)
        {
            try
            {
                var result = await _collection.FindAsync(o => o.OrderId == id);
                return _mapper.Map<Order>(result.FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.FindAsync)}");
                throw;
            }
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await _collection
                    .InsertOneAsync(_mapper.Map<OrderDTO>(order));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.InsertOneAsync)}");
                throw;
            }
        }

        public async Task<Order> CancelOrderAsync(string id)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<OrderDTO>(p => p.OrderId == id);
                var update = new UpdateDefinitionBuilder<OrderDTO>().Set(p => p.Status, OrderStatusDTO.Cancelled);

                return _mapper.Map<Order>(await _collection.FindOneAndUpdateAsync<OrderDTO>(filter, update));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.FindOneAndUpdateAsync)}");
                throw;
            }
        }

        public async Task UpdateDeliveryAddressAsync(string id, Address newAddress)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<OrderDTO>(p => p.OrderId == id);
                var update = new UpdateDefinitionBuilder<OrderDTO>().Set(p => p.DeliveryAddress, _mapper.Map<AddressDTO>(newAddress));

                await _collection.FindOneAndUpdateAsync<OrderDTO>(filter, update);
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
                var filter = new ExpressionFilterDefinition<OrderDTO>(p => p.OrderId == id);
                var update = new UpdateDefinitionBuilder<OrderDTO>().Set(p => p.Items, _mapper.Map<List<OrderItemDTO>>(items));

                return _mapper.Map<Order>(await _collection.FindOneAndUpdateAsync<OrderDTO>(filter, update));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.FindOneAndUpdateAsync)}");
                throw;
            }
        }

        public async Task ConfirmOrderAsync(string orderId)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<OrderDTO>(p => p.OrderId == orderId);
                var update = new UpdateDefinitionBuilder<OrderDTO>().Set(p => p.Status, OrderStatusDTO.Confirmed);

                await _collection.FindOneAndUpdateAsync<OrderDTO>(filter, update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.FindOneAndUpdateAsync)}");
                throw;
            }
        }

        public async Task RefuseOrderAsync(string orderId)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<OrderDTO>(p => p.OrderId == orderId);
                var update = new UpdateDefinitionBuilder<OrderDTO>().Set(p => p.Status, OrderStatusDTO.Refused);

                await _collection.FindOneAndUpdateAsync<OrderDTO>(filter, update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<OrderDTO>.FindOneAndUpdateAsync)}");
                throw;
            }
        }
    }
}