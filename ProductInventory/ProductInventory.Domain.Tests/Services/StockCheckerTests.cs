// using System.Threading.Tasks;
// using Common.Messaging.RabbitMq;
// using Moq;
// using OrderManagement.Domain.Services;
// using ProductInventory.Domain.Repository;
// using ProductInventory.Domain.Tests.Support.Factories;
// using TestStack.BDDfy;
// using Xunit;
//
// namespace ProductInventory.Domain.Tests.Services
// {
//     public class ProductServiceTests
//     {
//         private readonly Mock<IProductRepository> _repository = new Mock<IProductRepository>();
//         private readonly Mock<ISystemBus> _bus = new Mock<ISystemBus>();
//         private OrderService _service;
//         
//         public ProductServiceTests()
//         {
//             _service = new OrderService(_repository.Object, _bus.Object);
//         }
//         
//         [Theory]
//         [InlineData(1)]
//         [InlineData(2)]
//         public void FulfillOrder_EnoughStock(int amount)
//         {
//             this.Given((s) => s.AProductWithEnoughStock())
//                 .When(s => s.OrderNeedsToBeFulfilled())
//                 .Then(s => s.UpdatesInventoryWithCorrectAmount(amount))
//                 .And(s => s.SignalsOrderCanBeFulfilled())
//                 .BDDfy();
//         }
//         
//         [Theory]
//         [InlineData(1)]
//         [InlineData(2)]
//         public void FulfillOrder_InsufficientStock(int amount)
//         {
//             this.Given((s) => s.AProductWithInsufficientStock())
//                 .When(s => s.OrderNeedsToBeFulfilled())
//                 .Then(s => s.SignalsOrderCannotBeFulfilled())
//                 .BDDfy();
//         }
//
//         public void AProductWithEnoughStock()
//         {
//             _repository
//                 .Setup(x => x.GetProductAsync(It.IsAny<string>()))
//                 .ReturnsAsync(ProductFactory.CreateWithStock());
//         }
//
//         public void AProductWithInsufficientStock()
//         {
//             _repository
//                 .Setup(x => x.GetProductAsync(It.IsAny<string>()))
//                 .ReturnsAsync(ProductFactory.CreateWithoutStock());
//         }
//
//         public async Task OrderNeedsToBeFulfilled()
//         {
//             await _service.FulfillOrderAsync();
//         }
//
//         public void UpdatesInventoryWithCorrectAmount(int amount)
//         {
//         }
//
//         public void SignalsOrderCanBeFulfilled()
//         {
//         }
//
//         public void SignalsOrderCannotBeFulfilled()
//         {
//             
//         }
//     }
// }