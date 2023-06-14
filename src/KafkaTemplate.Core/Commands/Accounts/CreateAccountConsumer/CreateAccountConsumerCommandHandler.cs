using MediatR;

namespace KafkaTemplate.Core.Commands.Accounts.CreateAccountConsumer
{
    public class CreateAccountConsumerCommandHandler
        : IRequestHandler<CreateAccountConsumerCommand, Unit>
    {
        public async Task<Unit> Handle(CreateAccountConsumerCommand request, CancellationToken cancellationToken)
        {
            var a = request;
            return Unit.Value;
        }
    }
}
