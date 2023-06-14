using Confluent.Kafka;
using Confluent.SchemaRegistry;

namespace KafkaTemplate.Messaging.Kafka.Consumer
{
    public class KafkaConsumerConfig
    {
        public const string ConfigurationSectionKey = "KafkaConsumer";

        public ConsumerConfig? ConsumerConfig { get; set; }
        public SchemaRegistryConfig? SchemaRegistryConfig { get; set; }
    }
}
