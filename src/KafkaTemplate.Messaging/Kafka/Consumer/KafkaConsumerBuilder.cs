using Avro.Specific;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Options;

namespace KafkaTemplate.Messaging.Kafka.Consumer
{
    public interface IKafkaConsumerBuilder<TKey, TValue>
    {
        IConsumer<TKey, TValue> Build();
    }
    public class KafkaConsumerBuilder<TKey, TValue> : IKafkaConsumerBuilder<TKey, TValue>
        where TKey: class, ISpecificRecord
        where TValue : class, ISpecificRecord
    {
        private readonly KafkaConsumerConfig _KafkaConsumerConfig;
        private readonly CachedSchemaRegistryClient _cachedSchemaRegistryClient;

        public KafkaConsumerBuilder(IOptions<KafkaConsumerConfig> kafkaConsumerConfig)
        {
            _KafkaConsumerConfig = kafkaConsumerConfig.Value
                ?? throw new ArgumentNullException(nameof(kafkaConsumerConfig));

            _cachedSchemaRegistryClient = new CachedSchemaRegistryClient(_KafkaConsumerConfig.SchemaRegistryConfig);
        }

        public IConsumer<TKey, TValue> Build()
        {
            var consumerBuilder = new ConsumerBuilder<TKey, TValue>(_KafkaConsumerConfig.ConsumerConfig);
            var kafkaAvroKeyDeserializer = new AvroDeserializer<TKey>(_cachedSchemaRegistryClient).AsSyncOverAsync();
            var kafkaAvroValueDeserializer = new AvroDeserializer<TValue>(_cachedSchemaRegistryClient).AsSyncOverAsync();

            return consumerBuilder
                .SetKeyDeserializer(kafkaAvroKeyDeserializer)
                .SetValueDeserializer(kafkaAvroValueDeserializer)
                .Build();
        }
    } 
}
