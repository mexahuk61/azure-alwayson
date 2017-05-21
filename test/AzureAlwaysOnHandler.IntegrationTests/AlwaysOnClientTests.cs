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
    public class AlwaysOnClientTests
    {
        private const string ExistingPath = "/";
        private const string NotExistingPath = "/notfound";

        private readonly HttpClient _client;

        public AlwaysOnClientTests()
        {
            IWebHostBuilder builder = new WebHostBuilder()
                .UseStartup<Startup>();
            var server = new TestServer(builder);

            _client = server.CreateClient();
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("AlwaysOn");
        }

        [Fact]
        public async Task Status_code_should_be_succeed_for_existing_path()
        {
            HttpResponseMessage response = await _client.GetAsync(ExistingPath);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Headers_should_have_expected_values_for_existing_path()
        {
            HttpResponseMessage response = await _client.GetAsync(ExistingPath);

            response.Content.Headers.ContentType.MediaType.Should().Be("text/plain");
        }

        [Fact]
        public async Task Content_should_have_expected_value_for_existing_path()
        {
            HttpResponseMessage response = await _client.GetAsync(ExistingPath);
            string content = await response.Content.ReadAsStringAsync();

            content.Should().Be("I am alive!");
        }

        [Fact]
        public async Task Status_code_should_be_succeed_for_not_existing_path()
        {
            HttpResponseMessage response = await _client.GetAsync(NotExistingPath);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Headers_should_have_expected_values_for_not_existing_path()
        {
            HttpResponseMessage response = await _client.GetAsync(NotExistingPath);

            response.Content.Headers.ContentType.MediaType.Should().Be("text/plain");
        }

        [Fact]
        public async Task Content_should_have_expected_value_for_not_existing_path()
        {
            HttpResponseMessage response = await _client.GetAsync(NotExistingPath);
            string content = await response.Content.ReadAsStringAsync();

            content.Should().Be("I am alive!");
        }
    }
}