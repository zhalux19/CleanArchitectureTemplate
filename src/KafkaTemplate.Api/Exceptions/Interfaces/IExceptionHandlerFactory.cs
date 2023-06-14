namespace KafkaTemplate.Api.Exceptions.Interfaces
{
    public interface IExceptionHandlerFactory
    {
        IExceptionHandler Create(Exception exception);
    }
}
