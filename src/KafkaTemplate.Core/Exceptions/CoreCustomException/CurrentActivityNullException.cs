using KafkaTemplate.Core.Exceptions.AbstractExceptions;

namespace KafkaTemplate.Core.Exceptions.CoreCustomException
{
    public class CurrentActivityNullException : DefaultException
    {
        public CurrentActivityNullException() : base("Current activity is null")
        {
        }
    }
}
