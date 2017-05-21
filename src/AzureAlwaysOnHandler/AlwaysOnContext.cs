using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AzureAlwaysOnHandler
{
    internal class AlwaysOnContext
    {
        private readonly IHeaderDictionary _headers;
        private readonly HttpResponse _response;

        public AlwaysOnContext(HttpContext context)
        {
            _headers = context.Request.Headers;
            _response = context.Response;
        }

        public bool ValidateHeaders()
        {
            return _headers.TryGetValue("User-Agent", out StringValues userAgent) &&
                   userAgent.Contains("AlwaysOn");
        }

        public Task ReturnAliveAnswerAsync()
        {
            _response.StatusCode = (int) HttpStatusCode.OK;
            _response.ContentType = "text/plain";
            return _response.WriteAsync("I am alive!");
        }
    }
}