using MongoDB.Bson;

namespace ProductInventory.Repository.Mongo.Models
{
    public class ProductDTO
    {
        public ObjectId _id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}