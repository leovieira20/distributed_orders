using System;

namespace OrderList.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}