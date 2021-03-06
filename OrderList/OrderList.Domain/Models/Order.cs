﻿using System.Collections.Generic;

namespace OrderList.Domain.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}