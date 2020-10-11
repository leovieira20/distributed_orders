using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;
using ServiceStack.Redis;

namespace OrderList.Repository.Redis
{
    public class RedisOrderRepository : IOrderRepository
    {
        private readonly RedisManagerPool _manager;

        public RedisOrderRepository(IOptions<RedisConfiguration> options)
        {
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
                Console.WriteLine(e);
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
                Console.WriteLine(e);
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
                Console.WriteLine(e);
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
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                throw;
            }
        }
    }
}