using MediatR;

namespace KafkaTemplate.Core.Commands.Accounts.CreateAccountConsumer
{
    public class CreateAccountConsumerCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
