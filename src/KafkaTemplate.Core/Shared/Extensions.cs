using FluentValidation;
using KafkaTemplate.Core.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KafkaTemplate.Core.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviours<,>));
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            return services;
        }

        public static JsonSerializerOptions ConfigureJsonSerializerOptions(this JsonSerializerOptions options)
        {
            options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; 
            options.PropertyNameCaseInsensitive= true;
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }
    }
}
