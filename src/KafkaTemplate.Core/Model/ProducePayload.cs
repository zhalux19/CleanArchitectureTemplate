namespace KafkaTemplate.Core.Model
{
    public record ProducePayload<TKey, TValue>(TKey Key, TValue Value, string TopicName, string EventType, CancellationToken CancellationToken);
}
