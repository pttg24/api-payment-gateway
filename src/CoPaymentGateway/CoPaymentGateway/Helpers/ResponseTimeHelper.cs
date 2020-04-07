using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Prometheus;

namespace CoPaymentGateway.Helpers
{
    public class ResponseTimeHelper
    {
        private readonly RequestDelegate _next;

        public ResponseTimeHelper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HistogramConfiguration config = null;
            var sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();

            var histogram =
                Metrics
                    .CreateHistogram(
                        "api_response_time_seconds",
                        "API Response Time in seconds",
                        config);

            histogram
                .WithLabels(context.Request.Method, context.Request.Path)
                .Observe(sw.Elapsed.TotalSeconds);
        }
    }
}