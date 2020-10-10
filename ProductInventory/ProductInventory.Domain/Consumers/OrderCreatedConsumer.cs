using System;
using Common.Messaging.RabbitMq;
using ProductInventory.Domain.Events;
using ProductInventory.Domain.Events.Inbound;
using ProductInventory.Domain.Events.Outbound;
using ProductInventory.Domain.Exceptions;
using ProductInventory.Domain.Services;

namespace ProductInventory.Domain.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly IStockChecker _stockChecker;
        private readonly ISystemBus _systemBus;

        public OrderCreatedConsumer(IStockChecker stockChecker, ISystemBus systemBus)
        {
            _stockChecker = stockChecker;
            _systemBus = systemBus;
        }

        public async void Consume(OrderCreated message)
        {
            try
            {
                await _stockChecker.ReserveStockForItems(message.Order.Items);
                _systemBus.PostAsync(new OrderConfirmed { OrderId = message.Order.OrderId });
            }
            catch (NotEnoughStockForItemException e)
            {
                Console.WriteLine(e);
                _systemBus.PostAsync(new OrderRefused { OrderId = message.Order.OrderId });
            }
        }
    }
}