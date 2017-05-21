using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class AzureAlwaysOnExtensions
    {
        public static IApplicationBuilder UseAzureAlwaysOnHandler(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            return app.UseMiddleware<AzureAlwaysOnMiddleware>();
        }
    }
}