using KafkaTemplate.Api.Exceptions.Interfaces;
using KafkaTemplate.Core.Exceptions.AbstractExceptions;
using System.Net;

namespace KafkaTemplate.Api.Exceptions.Handlers
{
    public class ConflictExceptionHandler : AbstractHandler, IExceptionHandler
    {
        public ConflictExceptionHandler(IExceptionDetailsFactory exceptionDetailsFactory) : base(exceptionDetailsFactory)
        {
            ExceptionStatusCode = (int)HttpStatusCode.Conflict;
        }

        public Type Handledtype => typeof(ConflictException);
    }
}
