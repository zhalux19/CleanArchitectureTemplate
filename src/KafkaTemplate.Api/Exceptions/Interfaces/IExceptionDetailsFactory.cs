using Microsoft.AspNetCore.Mvc;

namespace KafkaTemplate.Api.Exceptions.Interfaces
{
    public interface IExceptionDetailsFactory
    {
        ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception);
    }
}
