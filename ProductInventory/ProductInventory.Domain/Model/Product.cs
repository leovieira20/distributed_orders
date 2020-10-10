using System;
using MongoDB.Bson;

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

        public ObjectId _id { get; set; }
        public string ProductId { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}