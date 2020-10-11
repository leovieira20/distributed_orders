using System;
using Common.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
using ProductInventory.Domain.Events;
using ProductInventory.Domain.Events.Inbound;
using ProductInventory.Domain.Events.Outbound;
using ProductInventory.Domain.Exceptions;
using ProductInventory.Domain.Services;

namespace ProductInventory.Domain.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;
        private readonly IStockChecker _stockChecker;
        private readonly ISystemBus _systemBus;

        public OrderCreatedConsumer(
            ILogger<OrderCreatedConsumer> logger,
            IStockChecker stockChecker, 
            ISystemBus systemBus)
        {
            _logger = logger;
            _stockChecker = stockChecker;
            _systemBus = systemBus;
        }

        public async void Consume(OrderCreated message)
        {
            try
            {
                await _stockChecker.ReserveStockForItems(message.Order.Items);
                _systemBus.Post(new OrderConfirmed { OrderId = message.Order.OrderId });
            }
            catch (NotEnoughStockForItemException e)
            {
                _logger.LogWarning(e, "Error trying to reserve stock items");
                _systemBus.Post(new OrderRefused { OrderId = message.Order.OrderId });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error trying to reserve stock items");
                throw;
            }
        }
    }
}