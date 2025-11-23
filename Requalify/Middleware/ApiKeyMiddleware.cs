using System.Threading.Tasks;

namespace Requalify.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY_HEADER = "X-API-KEY";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Permite HealthCheck SEM API KEY
            if (context.Request.Path.StartsWithSegments("/health"))
            {
                await _next(context);
                return;
            }

            // Verifica API KEY
            if (!context.Request.Headers.TryGetValue(APIKEY_HEADER, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key missing");
                return;
            }

            var apiKey = context.RequestServices.GetRequiredService<IConfiguration>()
                .GetValue<string>("ApiKey");

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }
    }
}
