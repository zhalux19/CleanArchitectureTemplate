using Avro.Specific;
using Confluent.Kafka;
using KafkaTemplate.Core.Model;
using KafkaTemplate.Core.Utilities;
using KafkaTemplate.Messaging.Kafka.Producer.Exceptions;
using System.Diagnostics;
using System.Text;

namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public interface ISpecificRecordProducerService<TKey, TValue>
        where TValue : ISpecificRecord where TKey : ISpecificRecord
    {
        Task<MessageDeliveryResult> ProduceSpecificRecordAsync(ProducePayload<TKey, TValue> producePayload);
    }

    public class SpecificRecordProducerService<TKey, TValue> : ISpecificRecordProducerService<TKey, TValue>, IDisposable where TValue : ISpecificRecord where TKey : ISpecificRecord
    {
        private readonly Lazy<IProducer<TKey, TValue>> _cachedProducer;

        public SpecificRecordProducerService(IKafkaProducerBuilder<TKey, TValue> kafkaProducerBuilder)
        {
            _cachedProducer = new Lazy<IProducer<TKey, TValue>>(() => kafkaProducerBuilder.Build());
        }

        private byte[] GetBytesFromString(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public async Task<MessageDeliveryResult> ProduceSpecificRecordAsync(ProducePayload<TKey, TValue> producePayload)
        {
            var message = GetkafkaMessage(producePayload);
            var deliveryResult = await _cachedProducer.Value.ProduceAsync(producePayload.TopicName, message, producePayload.CancellationToken);
            if (deliveryResult.Status == PersistenceStatus.NotPersisted) {
                throw new MessageDeliveryFailedException();
            }
            return (MessageDeliveryResult)Enum.ToObject(typeof(MessageDeliveryResult), deliveryResult.Status);

        }

        private Message<TKey, TValue> GetkafkaMessage(ProducePayload<TKey, TValue> producePayload)
        {
            var message = new Message<TKey, TValue>
            {
                Key = producePayload.Key,
                Value = producePayload.Value,
                Headers = new Headers()
            };

            message.Headers.Add(MessagingConstants.KafkaMessageHeaders.EventType, GetBytesFromString(producePayload.EventType));
            message.Headers.Add(MessagingConstants.KafkaMessageHeaders.TraceId, GetBytesFromString(TraceIdUtility.GetTraceId(Activity.Current)));

            return message;
        }

        public void Dispose()
        {
            if (_cachedProducer.IsValueCreated) _cachedProducer.Value.Dispose();
        }

    }
}
