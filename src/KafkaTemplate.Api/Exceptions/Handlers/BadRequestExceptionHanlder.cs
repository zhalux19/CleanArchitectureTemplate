using KafkaTemplate.Api.Exceptions.Interfaces;
using KafkaTemplate.Core.Exceptions.AbstractExceptions;
using System.Net;

namespace KafkaTemplate.Api.Exceptions.Handlers
{
    public class BadRequestExceptionHanlder : AbstractHandler, IExceptionHandler
    {
        public BadRequestExceptionHanlder(IExceptionDetailsFactory exceptionDetailsFactory) : base(exceptionDetailsFactory)
        {
            ExceptionStatusCode = (int)HttpStatusCode.BadRequest;
        }

        public Type Handledtype => typeof(BadRequestException);
    }
}
