namespace KafkaTemplate.Api.Exceptions.Interfaces
{
    public interface IExceptionHandler
    {
        Type Handledtype { get; }
        Task Handle(HttpContext context, Exception exception);
    }
}
