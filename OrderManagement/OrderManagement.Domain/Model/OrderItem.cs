using System;

namespace OrderManagement.Domain.Model
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int quantity { get; set; }
    }
}