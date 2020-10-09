using OrderManagement.Domain.Model;

namespace ProductInventory.Domain.Tests.Support.Factories
{
    public static class ProductFactory
    {
        public static Product CreateWithStock()
        {
            return new Product();
        }
        
        public static Product CreateWithoutStock()
        {
            return new Product();
        }
    }
}