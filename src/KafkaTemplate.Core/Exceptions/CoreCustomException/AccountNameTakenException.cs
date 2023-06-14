using KafkaTemplate.Core.Exceptions.AbstractExceptions;

namespace KafkaTemplate.Core.Exceptions.CoreCustomException
{
    public class AccountNameTakenException : ConflictException
    {
        public AccountNameTakenException(string message) : base(message)
        {
        }
    }
}
