using KafkaTemplate.Messaging.Kafka.Consumer.ConsumerCommandBuilder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaTemplate.Messaging.Kafka.Consumer
{
    public static class KafkaConsumerExtensions
    {
        public static IServiceCollection AddKafkaConsumer(
            this IServiceCollection services, 
            IConfiguration configuration) 
        {
            services.AddOptions<KafkaConsumerConfig>()
                .Bind(configuration.GetSection(KafkaConsumerConfig.ConfigurationSectionKey));

            services.AddTransient<IKafkaMessageConsumerFactory>(serviceProvider => new KafkaMessageConsumerFactory(serviceProvider));

            services.AddSingleton(typeof(IKafkaConsumerBuilder<,>), typeof(KafkaConsumerBuilder<,>));

            services.AddSingleton(typeof(IKafkaMessageComsumer<,>), typeof(KafkaMessageConsumer<,>));

            services.AddSingleton<IConsumerCommandBuilderPicker, ConsumerCommandBuilderPicker>();
            services.AddSingleton<IConsumerCommandBuilder, AccountCreatedMessageCommandBuilder>();

            services.AddHostedService<BackgroundKafkaConsumer>();
            return services;
        }
    }
}
