using Newtonsoft.Json;

namespace KafkaTemplate.Core.Utilities
{
    public static class ObjectToStringUtility
    {
        public static string ObjectToString(Object obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            });
        }
    }
}
