namespace KafkaTemplate.Core.Exceptions.AbstractExceptions
{
    public abstract class DefaultException: Exception
    {
        protected DefaultException(): base("Internal server error") { }
        protected DefaultException(string message) : base(message) { }
        protected DefaultException(string message, Exception innterException) : base(message, innterException)
        {
        }
    }
}
