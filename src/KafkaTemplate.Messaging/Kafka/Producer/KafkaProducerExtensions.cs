using KafkaTemplate.Core.Interfaces;
using KafkaTemplate.Messaging.Kafka.Producer.ProduceHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaTemplate.Messaging.Kafka.Producer
{
    public static class KafkaProducerExtensions
    {
        public static IServiceCollection AddKafkaProducer(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddOptions<KafkaProducerConfig>()
                .Bind(configuration.GetSection(KafkaProducerConfig.ConfigurationSectionKey));

            services.AddSingleton(typeof(IKafkaProducerBuilder<,>), typeof(KafkaProducerBuilder<,>));
            services.AddSingleton(typeof(IMessageProducer<,>), typeof(KafkaMessageProducer<,>));
            services.AddSingleton(typeof(ISpecificRecordProducerService<,>), typeof(SpecificRecordProducerService<,>));
            services.AddSingleton(typeof(IProducerHandlerFactory<,>), typeof(ProducerHandlerFactory<,>));
            services.AddSingleton(typeof(IProducerHandler<,>), typeof(AccountProducerHandler<,>));
            return services;
        }
    }
}
