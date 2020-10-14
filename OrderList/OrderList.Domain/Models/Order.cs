using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace OrderList.Domain.Models
{
    public class Order
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}