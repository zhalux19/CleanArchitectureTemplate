using KafkaTemplate.Core.Interfaces;
using KafkaTemplate.Core.Model;
using MediatR;

namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public class KafkaMessageProducer<TKey, TValue> : IMessageProducer<TKey, TValue>
    {
        private readonly IProducerHandlerFactory<TKey, TValue> _producerFactory;

        public KafkaMessageProducer( IProducerHandlerFactory<TKey, TValue> producerFactory)
        {
            _producerFactory = producerFactory;
        }

        public async Task<MessageDeliveryResult> ProduceAsync(ProducePayload<TKey, TValue> producePayload)
        {
            var handler = _producerFactory.CreateProducerHandler(producePayload.TopicName);
            return await handler.Handle(producePayload);


            //var message = GetkafkaMessage(producePayload);
            //var deliveryResult = await _cachedProducer.Value.ProduceAsync(producePayload.TopicName, message, producePayload.CancellationToken);
            //return (MessageDeliveryResult)Enum.ToObject(typeof(MessageDeliveryResult), deliveryResult.Status);
        }

        //public Message<TKey, TValue> GetkafkaMessage(ProducePayload<TKey, TValue> producePayload) 
        //{
        //    var message = new Message<TKey, TValue>
        //    {
        //        Key = producePayload.Key,
        //        Value = producePayload.Value,
        //        Headers = new Headers()
        //    };

        //    message.Headers.Add(MessagingConstants.KafkaMessageHeaders.EventType, GetBytesFromString(producePayload.EventType));
        //    message.Headers.Add(MessagingConstants.KafkaMessageHeaders.TraceId, GetBytesFromString(TraceIdUtility.GetTraceId(Activity.Current)));

        //    return message;
        //}

        //public static byte[] GetBytesFromString(string str) {
        //    return  Encoding.UTF8.GetBytes(str);
        //}

    }
}
