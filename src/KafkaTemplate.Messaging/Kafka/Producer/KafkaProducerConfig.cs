using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public class KafkaProducerConfig
    {
        public const string ConfigurationSectionKey = "KafkaProducer";
        
        public ProducerConfig? ProducerConfig { get; set; }
        public SchemaRegistryConfig? SchemaRegistryConfig { get; set; }
        public AvroSerializerConfig? AvroSerializerConfig { get; set; }
    }
}
