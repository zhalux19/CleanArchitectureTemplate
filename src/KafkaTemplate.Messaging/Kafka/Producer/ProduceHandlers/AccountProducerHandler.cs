using AutoMapper;
using KafkaTemplate.Core.Entities;
using KafkaTemplate.Core.Model;
using KafkaTemplate.Core.Shared;
using KafkaTemplate.Messaging.Contracts;

namespace KafkaTemplate.Messaging.Kafka.Producer.ProduceHandlers
{
    public class AccountProducerHandler<TKey, TValue> : IProducerHandler<string, Account>
    {
        private readonly ISpecificRecordProducerService<AccountMessageKey, AccountMessage> _producerService;
        private readonly IMapper _mapper;

        public AccountProducerHandler(ISpecificRecordProducerService<AccountMessageKey, AccountMessage> producerService, IMapper mapper)
        {
            _producerService = producerService;
            _mapper = mapper;
        }

        public string TopicName => CoreConstants.MessageTopics.Account;

        public async Task<MessageDeliveryResult> Handle(ProducePayload<string, Account> producePayload)
        {
            //var message = _mapper.Map<AccountMessage>(producePayload.Value);
            var value = producePayload.Value;
            var message = new AccountMessage() { Id = value.Id, Name = value.Name, IsAdmin = value.IsAdmin };
            var key = new AccountMessageKey() { key = producePayload.Key };
            var kafkaPayload = new ProducePayload<AccountMessageKey, AccountMessage>(key, message, producePayload.TopicName, producePayload.EventType, producePayload.CancellationToken);
            var result = await _producerService.ProduceSpecificRecordAsync(kafkaPayload);
            return result;
        }
    }
}
