namespace KafkaTemplate.Core.Exceptions.AbstractExceptions
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException() : base("Bade request") { }
        protected BadRequestException(string message): base(message) { }
        protected BadRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
