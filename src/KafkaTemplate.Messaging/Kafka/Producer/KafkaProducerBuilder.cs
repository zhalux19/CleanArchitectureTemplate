using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Options;

namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public class KafkaProducerBuilder<TKey, TValue> : IKafkaProducerBuilder<TKey, TValue>
        where TKey: class, ISpecificRecord
        where TValue : class, ISpecificRecord
    {

        private readonly KafkaProducerConfig _kafkaProducerConfig;
        private readonly CachedSchemaRegistryClient _cachedSchemaRegistryClient;

        public KafkaProducerBuilder(IOptions<KafkaProducerConfig> kafkaProducerConfig)
        {
            _kafkaProducerConfig = kafkaProducerConfig?.Value ?? 
                throw new ArgumentNullException(nameof(kafkaProducerConfig));

            _cachedSchemaRegistryClient = new CachedSchemaRegistryClient(_kafkaProducerConfig.SchemaRegistryConfig);
        }

        public IProducer<TKey, TValue> Build()
        {

            var producerBuilder = new ProducerBuilder<TKey, TValue>(_kafkaProducerConfig.ProducerConfig);

            var kafkaAvroKeySerializer = new AvroSerializer<TKey>(_cachedSchemaRegistryClient, _kafkaProducerConfig.AvroSerializerConfig);
            var kafkaAvroValueSerializer = new AvroSerializer<TValue>(_cachedSchemaRegistryClient, _kafkaProducerConfig.AvroSerializerConfig);

            return producerBuilder
                .SetKeySerializer(kafkaAvroKeySerializer)
                .SetValueSerializer(kafkaAvroValueSerializer)
                .Build();
        }
    }
}
