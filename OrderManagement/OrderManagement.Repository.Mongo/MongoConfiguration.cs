namespace OrderManagement.Repository.Mongo
{
    public class MongoConfiguration
    {
        public const string Name = "Mongo";
        
        public string Host { get; set; }
        public int Port { get; set; }
    }
}