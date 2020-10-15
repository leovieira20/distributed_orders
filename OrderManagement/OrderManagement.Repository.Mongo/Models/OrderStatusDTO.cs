namespace OrderManagement.Repository.Mongo.Models
{
    public enum OrderStatusDTO
    {
        Created = 0,
        Cancelled = 1,
        Fulfilled = 2,
        Confirmed = 3,
        Refused = 4
    }
}