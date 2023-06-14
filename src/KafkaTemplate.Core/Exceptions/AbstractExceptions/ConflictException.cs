namespace KafkaTemplate.Core.Exceptions.AbstractExceptions
{
    public abstract class ConflictException : Exception
    {
        protected ConflictException(): base("Conflict") { }
        protected ConflictException(string message): base(message) { }
        protected ConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
