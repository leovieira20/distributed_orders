using System.Collections.Generic;
using System.Threading.Tasks;
using ProductInventory.Domain.Model;

namespace ProductInventory.Domain.Repository
{
    public interface IProductRepository
    {
        Task<Product> Get(string id);
        Task<List<Product>> GetAll();
        Task Update(Product product);
        Task Create(Product product);
        Task BulkUpdate(List<Product> productsToUpdate);
    }
}