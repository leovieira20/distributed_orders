using System;
using Common.Messaging.RabbitMq;
using ProductInventory.Domain.Events;
using ProductInventory.Domain.Services;

namespace ProductInventory.Domain.Consumers
{
    public interface IOrderCreatedConsumer
    {
        void Consume(OrderCreated message);
    }

    public class OrderCreatedConsumer : IOrderCreatedConsumer
    {
        private readonly IStockChecker _stockChecker;
        private readonly ISystemBus _systemBus;

        public OrderCreatedConsumer(IStockChecker stockChecker, ISystemBus systemBus)
        {
            _stockChecker = stockChecker;
            _systemBus = systemBus;
        }

        public void Consume(OrderCreated message)
        {
            try
            {
                _stockChecker.ReserveStockForItems(message.Order.Items);
                _systemBus.PostAsync(new OrderConfirmed { OrderId = message.Order.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _systemBus.PostAsync(new OrderRefused { OrderId = message.Order.Id });
            }
        }
    }
}