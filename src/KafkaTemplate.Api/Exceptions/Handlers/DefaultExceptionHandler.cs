using KafkaTemplate.Api.Exceptions.Interfaces;
using KafkaTemplate.Core.Exceptions.AbstractExceptions;
using System.Net;

namespace KafkaTemplate.Api.Exceptions.Handlers
{
    public class DefaultExceptionHandler : AbstractHandler, IExceptionHandler
    {
        public DefaultExceptionHandler(IExceptionDetailsFactory exceptionDetailsFactory) : base(exceptionDetailsFactory)
        {
            ExceptionStatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public Type Handledtype => typeof(DefaultException);

    }
}
