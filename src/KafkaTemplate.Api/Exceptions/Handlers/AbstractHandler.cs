using KafkaTemplate.Api.Exceptions.Interfaces;
using KafkaTemplate.Core.Shared;
using System.Text.Json;

namespace KafkaTemplate.Api.Exceptions.Handlers
{
    public abstract class AbstractHandler
    {
        private readonly IExceptionDetailsFactory _exceptionDetailsFactory;
        protected int ExceptionStatusCode;

        protected AbstractHandler(IExceptionDetailsFactory exceptionDetailsFactory)
        {
            _exceptionDetailsFactory = exceptionDetailsFactory;
        }

        public async Task Handle(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = ExceptionStatusCode;
            context.Response.ContentType = "application/json";
            var problemDetails = _exceptionDetailsFactory.CreateProblemDetails(context, exception);
            var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions().ConfigureJsonSerializerOptions());
            await context.Response.WriteAsync(json);
        }
    }
}
