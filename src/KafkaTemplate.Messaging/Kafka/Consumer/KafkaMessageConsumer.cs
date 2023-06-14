using Avro.Specific;
using Confluent.Kafka;
using KafkaTemplate.Messaging.Kafka.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace KafkaTemplate.Messaging.Kafka.Consumer
{
    public interface IKafkaMessageComsumer<TKey, TValue> 
    {
        void ConsumeAsync(string topic, CancellationToken cancellationToken);
    }
    public class KafkaMessageConsumer<TKey, TValue> : IKafkaMessageComsumer<TKey, TValue>
        where TKey : ISpecificRecord
        where TValue : ISpecificRecord
    {
        private readonly IKafkaConsumerBuilder<TKey, TValue> _consumerBuilder;
        private readonly ILogger<KafkaMessageConsumer<TKey, TValue>> _logger;
        private readonly IConsumerCommandBuilderPicker _consumerCommandBuilderPicker;
        private readonly IMediator _mediator;

        public KafkaMessageConsumer(IKafkaConsumerBuilder<TKey, TValue> consumerBuilder, 
            ILogger<KafkaMessageConsumer<TKey, TValue>> logger, 
            IConsumerCommandBuilderPicker consumerCommandBuilderPicker, 
            IMediator mediator)
        {
            _consumerBuilder = consumerBuilder;
            _logger = logger;
            _consumerCommandBuilderPicker = consumerCommandBuilderPicker;
            _mediator = mediator;
        }

        public async void ConsumeAsync(string topic, CancellationToken cancellationToken)
        {
            using (var consumer = _consumerBuilder.Build())
            {
                try
                {
                    consumer.Subscribe(topic);
                    _logger.LogInformation($"Consume topic. Name: {consumer.Name}. MemberId: {consumer.MemberId}");

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        try
                        {
                            var consumerReslt = consumer.Consume(cancellationToken);
                            if (consumerReslt != null && consumerReslt.IsPartitionEOF)
                            {
                                _logger.LogInformation($"Consume. Topic: {consumerReslt.Topic}, MemberId: {consumer.MemberId}, IsPartitionEOF: true, Topic-Offset:{consumerReslt.Offset.Value}, Partition: {consumerReslt.Partition.Value}");
                                continue;
                            }
                            var validationResult = IsMessageValid(consumerReslt!);
                            if (!validationResult.isValid)
                            {
                                _logger.LogError($"Consume Message not valid. Topic: {topic}, MemberId: {consumer.MemberId}, Error: {validationResult.error}");
                            }
                            else
                            {
                                var traceId = GetHeaderValue(consumerReslt!, MessagingConstants.KafkaMessageHeaders.TraceId);
                                var eventType = GetHeaderValue(consumerReslt!, MessagingConstants.KafkaMessageHeaders.EventType);

                                var keyString = consumerReslt?.Key.RecordToString();
                                var valueString = consumerReslt?.Value.RecordToString();

                                _logger.LogInformation($"Consume. Topic: {topic}, MemberId: {consumer.MemberId}. TraceId: {traceId}. EventType: {eventType}. Key: {keyString}. Value: {valueString}");
                                var cosumerCommandBuilder = _consumerCommandBuilderPicker.Create(eventType!);
                                var command = cosumerCommandBuilder.Build(consumerReslt.Value, traceId, cancellationToken);
                                await _mediator.Send(command);
                            }
                            //consumer.StoreOffset(consumerReslt);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Consumer Unsuccess. Topic: {topic}, MemberId: {consumer.MemberId}, Error: {ex.Message}");
                        }
                    }
                }
                catch (OperationCanceledException ex) {
                    _logger.LogError(ex, $"Consume. Unsuccess: OperationCanceledException: Topic: {topic}, MemberId: {consumer.MemberId}, Error:{ex.Message} ");
                    consumer.Close(); ;
                }
            }
        }


        private (bool isValid, string error) IsMessageValid(ConsumeResult<TKey, TValue> consumeResult)
        {
            var messageKey = consumeResult.Message.Key;
            if (messageKey is null || messageKey is not ISpecificRecord key) {
                return (false, "Message key is not in right format");
            }
            var messageValue = consumeResult.Message.Value;
            if (messageValue is null || messageValue is not ISpecificRecord value) {
                return (false, "Message value is not in right format");
            }

            if (!consumeResult.Message.Headers.Any(x => x.Key != MessagingConstants.KafkaMessageHeaders.TraceId))
            {
                return (false, "TraceId header is mssing");
            }

            if (!consumeResult.Message.Headers.Any(x => x.Key != MessagingConstants.KafkaMessageHeaders.EventType))
            {
                return (false, "EventType header is mssing");
            }
            //var traceId = GetHeaderValue(consumeResult, MessagingConstants.KafkaMessageHeaders.TraceId);
            return (true, string.Empty);
        }

        private string? GetHeaderValue(ConsumeResult<TKey, TValue> consumeResult, string key) {
            if (consumeResult.Message.Headers.TryGetLastBytes(key, out byte[] encodedValue))
            { 
                return Encoding.UTF8.GetString(encodedValue);
            }
            return null;
        }
    }
}
