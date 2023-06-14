using KafkaTemplate.Api.Exceptions.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KafkaTemplate.Api.Exceptions.Factories
{
    public class ExceptionDetailsFactory : IExceptionDetailsFactory
    {
        private readonly IWebHostEnvironment _env;

        public ExceptionDetailsFactory(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Title= exception.Message
            };

            var activity = Activity.Current;

            if(exception is not null && activity is not null){
                problemDetails.Extensions.Add("TraceId", activity?.TraceId.ToString());
                problemDetails.Extensions.Add("ActivityId", activity?.Id);
            }

            if (!_env.IsDevelopment()) { 
                return problemDetails;      
            }

            problemDetails.Title = exception?.Message;
            problemDetails.Type = exception?.GetType().Name;
            problemDetails.Instance = httpContext.Request.Path;
            problemDetails.Detail = exception?.ToString();

            return problemDetails;
        }
    }
}
