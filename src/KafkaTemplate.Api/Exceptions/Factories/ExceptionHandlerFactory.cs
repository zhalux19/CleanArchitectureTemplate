using KafkaTemplate.Api.Exceptions.Handlers;
using KafkaTemplate.Api.Exceptions.Interfaces;

namespace KafkaTemplate.Api.Exceptions.Factories
{
    public class ExceptionHandlerFactory : IExceptionHandlerFactory
    {
        private readonly IEnumerable<IExceptionHandler> _handlers;

        public ExceptionHandlerFactory(IEnumerable<IExceptionHandler> handlers)
        {
            _handlers = handlers;
        }

        public IExceptionHandler Create(Exception exception)
        {
            var exceptionType = exception.GetType();
            var handler = _handlers.SingleOrDefault(x => x.Handledtype != null && exceptionType.IsSubclassOf(x.Handledtype));

            if (handler != null) { return handler; }

            return _handlers.Single(x => x is DefaultExceptionHandler);
        }
    }
}
