using KafkaTemplate.Core.Shared;
using KafkaTemplate.Messaging.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaTemplate.Messaging.Kafka.Consumer
{
    public interface IKafkaMessageConsumerFactory
    {
        void StartConsmers(CancellationToken cancellationToken);
    }

    public class KafkaMessageConsumerFactory : IKafkaMessageConsumerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public KafkaMessageConsumerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void StartConsmers(CancellationToken cancellationToken)
        {
            
            var accountTopicConsunmer = _serviceProvider.GetRequiredService<IKafkaMessageComsumer<AccountMessageKey, AccountMessage>>();
            new Thread(() => accountTopicConsunmer.ConsumeAsync(CoreConstants.MessageTopics.Account, cancellationToken))
                .Start();
        }
    }
}
