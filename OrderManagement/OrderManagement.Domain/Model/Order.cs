using System;
using System.Collections.Generic;

namespace OrderManagement.Domain.Model
{
    public class Order
    {
        public Order()
        {
            Status = OrderStatus.Created;
        }
        
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}