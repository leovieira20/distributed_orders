using System;
using Common.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain.Events.Inbound;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Domain.Consumers
{
    public class OrderConfirmedConsumer : IConsumer<OrderConfirmed>
    {
        private readonly ILogger<OrderConfirmedConsumer> _logger;
        private readonly IOrderRepository _repository;

        public OrderConfirmedConsumer(
            ILogger<OrderConfirmedConsumer> logger,
            IOrderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async void Consume(OrderConfirmed message)
        {
            try
            {
                await _repository.ConfirmOrderAsync(message.OrderId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to confirm order");
                throw;
            }
        }
    }
}