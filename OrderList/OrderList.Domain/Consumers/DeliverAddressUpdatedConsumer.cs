using System;
using Common.Messaging.RabbitMq;
using OrderList.Domain.Events.Inbound;
using OrderList.Domain.Repositories;

namespace OrderList.Domain.Consumers
{
    public class DeliverAddressUpdatedConsumer : IConsumer<DeliveryAddressUpdated>
    {
        private readonly IOrderRepository _repository;

        public DeliverAddressUpdatedConsumer(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public void Consume(DeliveryAddressUpdated message)
        {
            try
            {
                var order = _repository.Get(message.OrderId);
                order.DeliveryAddress = message.NewAddress;
                _repository.Update(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}