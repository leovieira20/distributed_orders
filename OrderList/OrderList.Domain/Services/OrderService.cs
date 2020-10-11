using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Services
{
    public interface IOrderService
    {
        Order Get(string id);
        List<Order> Get(int page, int size);
    }

    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _repository;

        public OrderService(
            ILogger<OrderService> logger,
            IOrderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Order Get(string id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error while trying to get order: {id}");
                throw;
            }
        }

        public List<Order> Get(int page, int size)
        {
            try
            {
                return _repository.Get(page, size);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error while trying to get orders: page: {page}, size: {size}");
                throw;
            }
        }
    }
}