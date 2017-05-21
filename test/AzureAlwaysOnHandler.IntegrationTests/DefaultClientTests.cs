using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureAlwaysOnHandler.IntegrationTests.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace AzureAlwaysOnHandler.IntegrationTests
{
    public class DefaultClientTests
    {
        private const string ExistPath = "/";
        private const string NotExistPath = "/notfound";

        private readonly HttpClient _client;

        public DefaultClientTests()
        {
            IWebHostBuilder builder = new WebHostBuilder()
                .UseStartup<Startup>();
            var server = new TestServer(builder);

            _client = server.CreateClient();
        }

        [Fact]
        public async Task Status_code_should_be_succeed()
        {
            HttpResponseMessage response = await _client.GetAsync(ExistPath);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Headers_should_have_expected_values()
        {
            HttpResponseMessage response = await _client.GetAsync(ExistPath);

            response.Content.Headers.ContentType.MediaType.Should().Be("text/html");
        }

        [Fact]
        public async Task Content_should_have_expected_values()
        {
            HttpResponseMessage response = await _client.GetAsync(ExistPath);
            string content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<body>");
            content.Should().Contain("I'm default page.");
        }

        [Fact]
        public async Task Status_code_should_be_not_found()
        {
            HttpResponseMessage response = await _client.GetAsync(NotExistPath);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Headers_should_not_have_expected_values()
        {
            HttpResponseMessage response = await _client.GetAsync(NotExistPath);

            response.Content.Headers.ContentType.Should().BeNull();
        }

        [Fact]
        public async Task Content_should_be_empty()
        {
            HttpResponseMessage response = await _client.GetAsync(NotExistPath);
            string content = await response.Content.ReadAsStringAsync();

            content.Should().BeEmpty();
        }
    }
}