using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;
using ServiceStack.Redis;

namespace OrderList.Repository.Redis
{
    public class RedisOrderRepository : IOrderRepository
    {
        private readonly ILogger<RedisOrderRepository> _logger;
        private readonly RedisManagerPool _manager;

        public RedisOrderRepository(
            ILogger<RedisOrderRepository> logger,
            IOptions<RedisConfiguration> options)
        {
            _logger = logger;
            var config = options.Value; 
            _manager = new RedisManagerPool($"{config.Host}:{config.Port}");
        }

        public Order Get(string id)
        {
            try
            {
                return _manager.GetClient()
                    .As<Order>()
                    .GetById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to get from redis, method: GetById");
                throw;
            }
        }

        public List<Order> Get(int page, int size)
        {
            try
            {
                var list = _manager.GetClient()
                    .As<Order>()
                    .GetEarliestFromRecentsList(page * size, size);
            
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to get orders from redis, method: GetEarliestFromRecentsList");
                throw;
            }
        }

        public void Create(Order order)
        {
            try
            {
                var client = _manager.GetClient()
                    .As<Order>();

                client.Store(order);
                client.AddToRecentsList(order);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to store in redis");
                throw;
            }
        }
        
        public void Delete(string orderId)
        {
            try
            {
                _manager.GetClient()
                    .As<Order>()
                    .DeleteById(orderId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to delete from redis");
                throw;
            }
        }

        public void Update(Order order)
        {
            try
            {
                _manager.GetClient()
                    .As<Order>()
                    .Store(order);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to update in redis");
                throw;
            }
        }
    }
}