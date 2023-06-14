using FluentValidation;

namespace KafkaTemplate.Core.Commands.Accounts.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(2);
        }
    }
}
