using System;
using System.Collections.Generic;

namespace OrderManagement.Domain.Model
{
    public class Order
    {
        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
            Status = OrderStatus.Created;
        }
        
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}