using KafkaTemplate.Api.Exceptions.Factories;
using KafkaTemplate.Api.Exceptions.Handlers;
using KafkaTemplate.Api.Exceptions.Interfaces;
using KafkaTemplate.Api.Middlewares;
using KafkaTemplate.Data.Mapping;
using KafkaTemplate.Messaging.Kafka.Consumer.Mapping;
using System.Reflection;

namespace KafkaTemplate.Api.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddAutoMapper(typeof(DataMapperProfile))
                .AddAutoMapper(typeof(MessageMapperProfile));
            //.AddAutoMapper(Data.AssemblyReference.Assembly);

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddTransient<ActivityMiddleware>();

            return services;
        }

        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            services.AddSingleton<IExceptionHandlerFactory, ExceptionHandlerFactory>();
            services.AddSingleton<IExceptionDetailsFactory, ExceptionDetailsFactory>();
            services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();
            services.AddSingleton<IExceptionHandler, BadRequestExceptionHanlder>();
            services.AddSingleton<IExceptionHandler, ConflictExceptionHandler>();
            return services;
        }
    }
}
