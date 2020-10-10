using System.Threading.Tasks;
using ProductInventory.Domain.Model;

namespace ProductInventory.Domain.Repository
{
    public interface IProductRepository
    {
        Task<Product> Get(string id);
        Task Update(Product product);
        Task Create(Product product);
    }
}