using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AzureAlwaysOnHandler.IntegrationTests.Setup
{
    public class Startup
    {
        private const string body =
@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>default page</title>        
</head>
<body>
    I'm default page.
</body>
</html>";

        public void Configure(IApplicationBuilder app)
        {
            app.UseAzureAlwaysOnHandler();
            app.Run(async context =>
            {
                if (context.Request.Path.Value != "/")
                {
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    return;
                }
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(body);
            });
        }
    }
}