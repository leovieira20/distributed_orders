namespace OrderList.Domain.Models
{
    public class Order
    {
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}