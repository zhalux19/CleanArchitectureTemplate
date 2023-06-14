using KafkaTemplate.Core.Model;

namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public interface IProducerHandler<TKey, TValue>
    {
        string TopicName { get; }
        Task<MessageDeliveryResult> Handle(ProducePayload<TKey, TValue> producePayload);
    }
}
