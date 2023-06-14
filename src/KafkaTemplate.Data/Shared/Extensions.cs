using KafkaTemplate.Core.Interfaces;
using KafkaTemplate.Data.Config;
using KafkaTemplate.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KafkaTemplate.Data.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.ConfigurationSectionKey));
            services.AddSingleton<IMongoDbOptions>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value);
            services.AddSingleton<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
