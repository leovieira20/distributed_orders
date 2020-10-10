using System.Collections.Generic;

namespace ProductInventory.Domain.Model
{
    public class Order
    {
        public string OrderId { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}