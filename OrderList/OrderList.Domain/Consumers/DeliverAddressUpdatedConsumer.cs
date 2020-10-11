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
        
        public async void Consume(DeliveryAddressUpdated message)
        {
            try
            {
                var order = await _repository.GetAsync(message.OrderId);
                order.DeliveryAddress = message.NewAddress;
                await _repository.UpdateDeliveryAddress(order, order.DeliveryAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}