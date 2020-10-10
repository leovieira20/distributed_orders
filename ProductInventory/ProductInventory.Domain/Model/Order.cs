using System.Collections.Generic;

namespace ProductInventory.Domain.Model
{
    public class Order
    {
        public string Id { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}