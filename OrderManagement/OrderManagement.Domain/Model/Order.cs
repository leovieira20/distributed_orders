using System.Collections.Generic;

namespace OrderManagement.Domain.Model
{
    public class Order
    {
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}