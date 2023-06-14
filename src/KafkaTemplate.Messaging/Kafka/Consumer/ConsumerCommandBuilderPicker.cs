using KafkaTemplate.Messaging.Kafka.Consumer.ConsumerCommandBuilder;

using Microsoft.Extensions.Logging;

namespace KafkaTemplate.Messaging.Kafka.Consumer
{
    public interface IConsumerCommandBuilderPicker
    {
        IConsumerCommandBuilder Create(string eventType);
    }

    public class ConsumerCommandBuilderPicker : IConsumerCommandBuilderPicker
    {
        private readonly IEnumerable<IConsumerCommandBuilder> _consumerCommandBuilders;
        private readonly ILogger<ConsumerCommandBuilderPicker> _logger;

        public ConsumerCommandBuilderPicker(IEnumerable<IConsumerCommandBuilder> consumerHandlers, ILogger<ConsumerCommandBuilderPicker> logger)
        {
            _consumerCommandBuilders = consumerHandlers;
            _logger = logger;
        }

        public IConsumerCommandBuilder Create(string eventType)
        {
            return _consumerCommandBuilders.Single(x => x.EventType == eventType);
        }
    }
}
