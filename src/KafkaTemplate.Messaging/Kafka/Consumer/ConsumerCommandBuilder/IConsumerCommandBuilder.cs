using Avro.Specific;
using MediatR;

namespace KafkaTemplate.Messaging.Kafka.Consumer.ConsumerCommandBuilder
{
    public interface IConsumerCommandBuilder
    {
        string EventType { get; }

        IRequest<Unit> Build(ISpecificRecord message, string traceId, CancellationToken cancellationToken);
    }
}
