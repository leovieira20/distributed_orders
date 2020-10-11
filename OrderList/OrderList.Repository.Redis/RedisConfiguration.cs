namespace OrderList.Repository.Redis
{
    public class RedisConfiguration
    {
        public const string Name = "Redis";
        
        public string Host { get; set; }
        public int Port { get; set; }
    }
}