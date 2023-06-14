namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public interface IProducerHandlerFactory<TKey, TValue>
    {
        IProducerHandler<TKey, TValue> CreateProducerHandler(string topicName);
    }

    public class ProducerHandlerFactory<TKey, TValue> : IProducerHandlerFactory<TKey, TValue>
     where TKey : class where TValue : class
    {
        private readonly IProducerHandler<TKey, TValue> _handlers;

        public ProducerHandlerFactory(IProducerHandler<TKey, TValue> handlers)
        {
            _handlers = handlers;
        }

        public IProducerHandler<TKey, TValue> CreateProducerHandler(string topicName)
        {
            return _handlers;
        }
    }

    //public class ProducerHandlerFactory<TKey, TValue> : IProducerHandlerFactory<TKey, TValue>
    //     where TKey : class where TValue : class
    //{
    //    private readonly IEnumerable<IProducerHandler<TKey, TValue>> _handlers;

    //    public ProducerHandlerFactory(IEnumerable<IProducerHandler<TKey, TValue>> handlers)
    //    {
    //        _handlers = handlers;
    //    }

    //    public IProducerHandler<TKey, TValue> CreateProducerHandler(string topicName)
    //    {
    //        var handler = _handlers.Single(x => x.TopicName == topicName);
    //        return handler;
    //    }
    //}
}
