using KafkaTemplate.Api.Exceptions.Interfaces;

namespace KafkaTemplate.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IExceptionHandlerFactory _exceptionHandlerFactory;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IExceptionHandlerFactory exceptionHandlerFactory)
        {
            _next = next;
            _logger = logger;
            _exceptionHandlerFactory = exceptionHandlerFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            var handler = _exceptionHandlerFactory.Create(exception);
            await handler.Handle(context, exception);
        }
    }
}
