using System;

namespace OrderManagement.Domain.Model
{
    public class OrderItem
    {
        public OrderItem()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}