using System;
using Common.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Consumers
{
    public class OrderRefusedConsumer : IConsumer<OrderRefused>
    {
        private readonly ILogger<OrderRefusedConsumer> _logger;
        private readonly IOrderRepository _repository;

        public OrderRefusedConsumer(
            ILogger<OrderRefusedConsumer> logger,
            IOrderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public async void Consume(OrderRefused message)
        {
            try
            {
                await _repository.RefuseOrderAsync(message.OrderId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to refuse order");
                throw;
            }
        }
    }
}