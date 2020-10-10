using System;
using System.Collections.Generic;

namespace OrderManagement.Domain.Model
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
            Status = OrderStatus.Created;
        }
        
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}