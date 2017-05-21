using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AzureAlwaysOnHandler;

namespace Microsoft.AspNetCore.Builder
{
    public class AzureAlwaysOnMiddleware
    {
        private readonly RequestDelegate _next;

        public AzureAlwaysOnMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var aliveContext = new AlwaysOnContext(context);
            if (!aliveContext.ValidateHeaders())
                return _next.Invoke(context);
            return aliveContext.ReturnAliveAnswerAsync();
        }
    }
}