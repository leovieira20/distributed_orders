using System.Collections.Generic;
using MongoDB.Bson;

namespace OrderManagement.Repository.Mongo.Models
{
    public class OrderDTO
    {
        public ObjectId _id { get; set; }
        public string OrderId { get; set; }
        public OrderStatusDTO Status { get; set; }
        public AddressDTO DeliveryAddress { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}