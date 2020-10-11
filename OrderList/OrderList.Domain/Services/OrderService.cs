using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrderList.Domain.Models;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Services
{
    public interface IOrderService
    {
        Task<Order> GetAsync(string id);
        Task<List<Order>> GetAsync(int page, int size);
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

        public async Task<Order> GetAsync(string id)
        {
            try
            {
                return await _repository.GetAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error while trying to get order: {id}");
                throw;
            }
        }

        public async Task<List<Order>> GetAsync(int page, int size)
        {
            try
            {
                return await _repository.GetAsync(page, size);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error while trying to get orders: page: {page}, size: {size}");
                throw;
            }
        }
    }
}