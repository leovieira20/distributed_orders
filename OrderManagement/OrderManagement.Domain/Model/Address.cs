using MongoDB.Bson;

namespace OrderManagement.Domain.Model
{
    public class Address
    {
        public ObjectId _id { get; set; }
        public string Street { get; set; }
    }
}