using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Requalify.Tests.Integration
{
    public class ApiHealthCheckTests :
        IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiHealthCheckTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task HealthCheck_DeveRetornar200()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/health");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
