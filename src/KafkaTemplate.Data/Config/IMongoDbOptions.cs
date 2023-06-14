namespace KafkaTemplate.Data.Config
{
    public interface IMongoDbOptions
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
