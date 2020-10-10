using System.Collections.Generic;
using System.Threading.Tasks;
using ProductInventory.Domain.Exceptions;
using ProductInventory.Domain.Model;
using ProductInventory.Domain.Repository;

namespace ProductInventory.Domain.Services
{
    public interface IStockChecker
    {
        Task ReserveStockForItems(IEnumerable<OrderItem> items);
    }
    
    public class StockChecker : IStockChecker
    {
        private readonly IProductRepository _repository;

        public StockChecker(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public async Task ReserveStockForItems(IEnumerable<OrderItem> items)
        {
            var products = new List<Product>();
            foreach (var i in items)
            {
                var p = await _repository.Get(i.ProductId);
                if (p.AvailableQuantity < i.Quantity)
                {
                    throw new NotEnoughStockForItemException(i.ProductId);
                }

                p.ReserveQuantity(i.Quantity);
                products.Add(p);
            }

            foreach (var p in products)
            {
                await _repository.Update(p);
            }
        }
    }
}