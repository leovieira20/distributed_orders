using System;

namespace ProductInventory.Domain.Model
{
    public class Product
    {
        public static Product CreateWithIdAndQuantity(string id, int quantity)
        {
            return new Product(id, quantity);
        }

        public Product()
        {
            ProductId = Guid.NewGuid().ToString();
        }

        private Product(string productId, int quantity)
        {
            ProductId = productId;
            AvailableQuantity = quantity;
        }

        public void ReserveQuantity(int quantity)
        {
            AvailableQuantity -= quantity;
            ReservedQuantity += quantity;
        }
        
        public void ReleaseQuantity(int quantity)
        {
            AvailableQuantity += quantity;
            ReservedQuantity -= quantity;
        }
        
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}