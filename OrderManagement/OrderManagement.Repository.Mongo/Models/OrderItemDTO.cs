namespace OrderManagement.Repository.Mongo.Models
{
    public class OrderItemDTO
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}