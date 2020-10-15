using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;
using ProductInventory.Repository.Mongo.Models;

namespace ProductInventory.Repository.Mongo
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly ILogger<MongoProductRepository> _logger;
        private readonly IMapper _mapper;
        private readonly string dbName = "distributed_orders";
        private readonly IMongoCollection<ProductDTO> _collection;

        public MongoProductRepository(
            ILogger<MongoProductRepository> logger,
            IOptions<MongoConfiguration> options,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            var config = options.Value;
            var client = new MongoClient($"mongodb://{config.Host}:{config.Port}/{dbName}");
            _collection = client
                .GetDatabase(dbName)
                .GetCollection<ProductDTO>("products");
        }

        public async Task<Product> GetAsync(string id)
        {
            try
            {
                var items = await _collection
                    .FindAsync(new ExpressionFilterDefinition<ProductDTO>(product => product.ProductId == id));

                return _mapper.Map<Product>(items.FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.FindAsync)}");
                throw;
            }
        }

        public async Task<List<Product>> GetAll()
        {
            try
            {
                var cursor = await _collection.FindAsync(Builders<ProductDTO>.Filter.Empty);
                return _mapper.Map<List<Product>>(await cursor.ToListAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.FindAsync)}");
                throw;
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<ProductDTO>(p => p.ProductId == product.ProductId);
                var updateDefinitionList = new List<UpdateDefinition<ProductDTO>>
                {
                    new UpdateDefinitionBuilder<ProductDTO>().Set(p => p.AvailableQuantity, product.AvailableQuantity),
                    new UpdateDefinitionBuilder<ProductDTO>().Set(p => p.ReservedQuantity, product.ReservedQuantity)
                };

                await _collection.FindOneAndUpdateAsync<ProductDTO>(filter, Builders<ProductDTO>.Update.Combine(updateDefinitionList));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.FindOneAndUpdateAsync)}");
                throw;
            }
        }

        public async Task CreateAsync(Product product)
        {
            try
            {
                await _collection
                    .InsertOneAsync(_mapper.Map<ProductDTO>(product));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.InsertOneAsync)}");
                throw;
            }
        }

        public async Task BulkUpdate(List<Product> productsToUpdate)
        {
            try
            {
                var models = new List<WriteModel<ProductDTO>>();

                foreach (var product in productsToUpdate)
                {
                    var filter = new ExpressionFilterDefinition<ProductDTO>(p => p.ProductId == product.ProductId);
                    var updateDefinitionList = new List<UpdateDefinition<ProductDTO>>
                    {
                        new UpdateDefinitionBuilder<ProductDTO>().Set(p => p.AvailableQuantity, product.AvailableQuantity),
                        new UpdateDefinitionBuilder<ProductDTO>().Set(p => p.ReservedQuantity, product.ReservedQuantity)
                    };
                    
                    var action = new UpdateOneModel<ProductDTO>(filter, Builders<ProductDTO>.Update.Combine(updateDefinitionList));
                    models.Add(action);
                }
                
                await _collection.BulkWriteAsync(models);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error trying to access mongo, method: {nameof(IMongoCollection<Order>.BulkWriteAsync)}");
                throw;
            }
        }
    }
}