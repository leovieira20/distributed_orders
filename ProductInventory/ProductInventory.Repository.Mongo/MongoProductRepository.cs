using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;

namespace ProductInventory.Repository.Mongo
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly string dbName = "distributed_orders";
        private readonly IMongoCollection<Product> _collection;

        public MongoProductRepository()
        {
            var client = new MongoClient($"mongodb://localhost:27018/{dbName}");
            _collection = client
                .GetDatabase(dbName)
                .GetCollection<Product>("products");
        }

        public async Task<Product> Get(string id)
        {
            try
            {
                var items = await _collection
                    .FindAsync(new ExpressionFilterDefinition<Product>(product => product.ProductId == id));

                return items.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                var filter = new ExpressionFilterDefinition<Product>(p => p.ProductId == product.ProductId);
                var updateDefinitionList = new List<UpdateDefinition<Product>>
                {
                    new UpdateDefinitionBuilder<Product>().Set(p => p.AvailableQuantity, product.AvailableQuantity),
                    new UpdateDefinitionBuilder<Product>().Set(p => p.ReservedQuantity, product.ReservedQuantity)
                };

                await _collection.FindOneAndUpdateAsync<Product>(filter, Builders<Product>.Update.Combine(updateDefinitionList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Create(Product product)
        {
            try
            {
                await _collection
                    .InsertOneAsync(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}