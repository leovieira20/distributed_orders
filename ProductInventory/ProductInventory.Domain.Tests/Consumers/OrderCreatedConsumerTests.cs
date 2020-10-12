using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Common.Messaging.RabbitMq;
using Microsoft.Extensions.Logging;
using Moq;
using ProductInventory.Domain.Consumers;
using ProductInventory.Domain.Events.Inbound;
using ProductInventory.Domain.Events.Outbound;
using ProductInventory.Domain.Exceptions;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Services;
using TestStack.BDDfy;
using Xunit;

namespace ProductInventory.Domain.Tests.Consumers
{
    public class OrderCreatedConsumerTests
    {
        private readonly Mock<ILogger<OrderCreatedConsumer>> _logger = new Mock<ILogger<OrderCreatedConsumer>>();
        private readonly Mock<IStockChecker> _stockChecker = new Mock<IStockChecker>();
        private readonly Mock<ISystemBus> _bus = new Mock<ISystemBus>();
        private readonly Fixture _fixture = new Fixture();
        private readonly OrderCreatedConsumer _consumer;

        public OrderCreatedConsumerTests()
        {
            _consumer = new OrderCreatedConsumer(
                _logger.Object, 
                _stockChecker.Object, 
                _bus.Object);
        }
        
        [Fact]
        public void ProductWithStock()
        {
            this.Given(s => s.AProductWithEnoughStock())
                .When(s => s.OrderNeedsToBeFulfilled())
                .Then(s => s.ConfirmsOrder())
                .BDDfy();
        }

        [Fact]
        public void ProductWithoutStock()
        {
            this.Given(s => s.AProductWithInsufficientStock())
                .When(s => s.OrderNeedsToBeFulfilled())
                .Then(s => s.RefusesOrder())
                .BDDfy();
        }
        
        public void AProductWithEnoughStock()
        {
            _stockChecker
                .Setup(x => x.ReserveStockForItemsAsync(It.IsAny<IEnumerable<OrderItem>>()))
                .Returns(Task.CompletedTask);
        }

        public void AProductWithInsufficientStock()
        {
            _stockChecker
                .Setup(x => x.ReserveStockForItemsAsync(It.IsAny<IEnumerable<OrderItem>>()))
                .Throws<NotEnoughStockForItemException>();
        }

        public async Task OrderNeedsToBeFulfilled()
        {
            _consumer.Consume(_fixture.Create<OrderCreated>());
        }

        public void ConfirmsOrder()
        {
            _bus.Verify(x => x.Post(It.IsAny<OrderConfirmed>()), Times.AtLeastOnce);
        }

        public void RefusesOrder()
        {
            _bus.Verify(x => x.Post(It.IsAny<OrderRefused>()), Times.AtLeastOnce);
        }
    }
}