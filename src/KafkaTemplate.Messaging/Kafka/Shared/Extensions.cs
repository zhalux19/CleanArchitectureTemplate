using KafkaTemplate.Messaging.Kafka.Consumer;
using KafkaTemplate.Messaging.Kafka.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaTemplate.Messaging.Kafka.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddKafkaProducer(configuration);
            services.AddKafkaConsumer(configuration);
            return services;
        }
    }
}
