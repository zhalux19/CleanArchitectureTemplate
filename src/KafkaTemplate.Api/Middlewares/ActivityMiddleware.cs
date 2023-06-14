using System.Diagnostics;

namespace KafkaTemplate.Api.Middlewares
{
    public class ActivityMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var activity = new Activity("RequestActivity");

            // Start the activity
            activity.Start();

            try
            {
                // Call the next middleware in the pipeline
                await next(context);
            }
            finally
            {
                // Stop the activity
                activity.Stop();
            }
        }
    }
}
