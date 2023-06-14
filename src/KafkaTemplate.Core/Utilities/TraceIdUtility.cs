using KafkaTemplate.Core.Exceptions.CoreCustomException;
using System.Diagnostics;

namespace KafkaTemplate.Core.Utilities
{
    public static class TraceIdUtility
    {
        public static string GetTraceId(Activity? currentActivity)
        {
            if (currentActivity != null)
            {
                return currentActivity.TraceId.ToString();
            }

            throw new CurrentActivityNullException();
        }
    }
}
