namespace OrderList.Domain.Models
{
    public class Order
    {
        public string Id => OrderId;
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
    }
}