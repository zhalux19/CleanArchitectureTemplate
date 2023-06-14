using ErrorOr;
using MediatR;

namespace KafkaTemplate.Core.Commands.Accounts.CreateAccount;

public record CreateAccountCommand : IRequest<ErrorOr<Unit>> 
{
    public string Name { get; set; }
    public bool IsAdmin { get; set; }
}