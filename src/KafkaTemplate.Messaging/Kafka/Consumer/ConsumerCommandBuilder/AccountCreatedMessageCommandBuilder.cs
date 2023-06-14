using AutoMapper;
using Avro.Specific;
using KafkaTemplate.Core.Commands.Accounts.CreateAccountConsumer;
using KafkaTemplate.Core.Shared;
using KafkaTemplate.Messaging.Contracts;
using MediatR;

namespace KafkaTemplate.Messaging.Kafka.Consumer.ConsumerCommandBuilder
{
    public class AccountCreatedMessageCommandBuilder : IConsumerCommandBuilder
    {
        private readonly IMapper _mapper;

        public AccountCreatedMessageCommandBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public string EventType => CoreConstants.EventTypes.AccountCreated;

        public IRequest<Unit> Build(ISpecificRecord message, string traceId, CancellationToken cancellationToken)
        {
            if (message is not AccountMessage accountMessage)
            {
                //TODO Create custom exception
                throw new Exception($"Account message casting error. TraceId {traceId}");
            }

            var command = _mapper.Map<CreateAccountConsumerCommand>(accountMessage);
            return command;
        }
    }
}
