using Avro.Specific;
using KafkaTemplate.Core.Utilities;
using Newtonsoft.Json;

namespace KafkaTemplate.Messaging.Kafka.Utilities
{
    public static class SpecificRecordToString
    {
        public static string RecordToString(this ISpecificRecord record)
        {
            return JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new IgnorePropertiesResolver(new[] { "Schema" })
            });
        }
    }
}
