using ErrorOr;
using KafkaTemplate.Core.Entities;
using KafkaTemplate.Core.Interfaces;
using KafkaTemplate.Core.Model;
using KafkaTemplate.Core.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KafkaTemplate.Core.Commands.Accounts.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ErrorOr<Unit>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<CreateAccountCommandHandler> _logger;
        private readonly IMessageProducer<string, Account> _messageProducer;

        public CreateAccountCommandHandler(
            IAccountRepository accountRepository, 
            ILogger<CreateAccountCommandHandler> logger, 
            IMessageProducer<string, Account> messageProducer)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            _messageProducer = messageProducer;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            //var existingAccount = _accountRepository.GetAccountByName(request.Name);

            //if (existingAccount is not null) {
            //    throw new AccountNameTakenException($"Account name taken {request.Name}");
            //}
            _logger.LogInformation("Saving data into db");
            var account = new Account {Name = request.Name, IsAdmin = request.IsAdmin };
            account = await _accountRepository.Create(account);
            var producePayload = new ProducePayload<string, Account>(
                account.Id!, account,
                CoreConstants.MessageTopics.Account,
                CoreConstants.EventTypes.AccountCreated,
                cancellationToken);
            var deliveryStatus = await _messageProducer.ProduceAsync(producePayload);
            return Unit.Value;
        }
    }
}
