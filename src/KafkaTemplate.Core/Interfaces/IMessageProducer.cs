using KafkaTemplate.Core.Model;

namespace KafkaTemplate.Core.Interfaces
{
    public interface IMessageProducer<TKey, TValue>
    {
        Task<MessageDeliveryResult> ProduceAsync(ProducePayload<TKey, TValue> producePayload);
    }
}
