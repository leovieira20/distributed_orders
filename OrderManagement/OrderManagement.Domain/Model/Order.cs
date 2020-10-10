using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace OrderManagement.Domain.Model
{
    public class Order
    {
        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
            Status = OrderStatus.Created;
        }

        public ObjectId _id { get; set; }
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}