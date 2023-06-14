using Confluent.Kafka;

namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public interface IKafkaProducerBuilder<TKey, TValue>
    {
        IProducer<TKey, TValue> Build();
    }
}
