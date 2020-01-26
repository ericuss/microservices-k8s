namespace Lanre.Stock.Api.IntegrationTests.Controllers.Stock
{
    using Lanre.Stock.Api.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;
    using FluentAssertions;

    public class CallToGetStockTests : IClassFixture<WebHostFixture>, IDisposable
    {
        readonly WebHostFixture _fixture;
        readonly HttpClient _client;

        public CallToGetStockTests(WebHostFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
        }

        public void Dispose() => _fixture.Output = null;

        [Fact]
        public async Task CallToGetProductsAndReturnOk()
        {
            var result = await _client.GetAsync("/api/stock");

            result.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CallToGetProductsAndReturnProducts()
        {
            var result = await _client.GetAsync("/api/stock");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            var desserialized = JsonConvert.DeserializeObject<List<ProductDto>>(content);
            desserialized.Should().NotBeEmpty();
        }
    }
}
