namespace Lanre.Products.Api.IntegrationTests.Core
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;
    using FluentAssertions;

    public abstract class TestCoreCrud : IClassFixture<WebHostFixture>, IDisposable
    {
        protected readonly WebHostFixture Fixture;
        protected readonly HttpClient Client;

        public TestCoreCrud(WebHostFixture fixture, ITestOutputHelper output)
        {
            Fixture = fixture;
            fixture.Output = output;
            Client = fixture.CreateClient();
        }

        public void Dispose() => Fixture.Output = null;

        protected virtual async Task<T> Get<T>(string endpoint, bool ensureStatusCode = true, int? statusCode = 200, bool notBeNull = true)
        {
            var result = await Client.GetAsync(endpoint);

            if (ensureStatusCode)
            {
                result.EnsureSuccessStatusCode();
            }

            if (statusCode.HasValue)
            {
                result.StatusCode.Should().Be(statusCode);
            }

            var content = await result.Content.ReadAsStringAsync();
            var desserialized = JsonConvert.DeserializeObject<T>(content);

            if (notBeNull)
            {
                desserialized.Should().NotBeNull();
            }

            return desserialized;
        }
    }
}
