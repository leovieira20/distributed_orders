namespace OrderList.Domain.Models
{
    public enum OrderStatus
    {
        Created = 0,
        Cancelled = 1,
        Fulfilled = 2,
        Confirmed = 3,
        Refused = 4
    }
}