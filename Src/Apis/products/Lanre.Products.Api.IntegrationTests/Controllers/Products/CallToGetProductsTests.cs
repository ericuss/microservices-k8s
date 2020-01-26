namespace Lanre.Products.Api.IntegrationTests.Controllers.Products
{
    using Lanre.Products.Api.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;
    using FluentAssertions;

    public class CallToGetProductsTests : IClassFixture<WebHostFixture>, IDisposable
    {
        private readonly WebHostFixture _fixture;
        private readonly HttpClient _client;

        public CallToGetProductsTests(WebHostFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
        }

        public void Dispose() => _fixture.Output = null;

        [Fact]
        public async Task CallToGetProductsAndReturnOk()
        {
            var result = await _client.GetAsync("/api/products");

            result.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CallToGetProductsAndReturnProducts()
        {
            var result = await _client.GetAsync("/api/products");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();
            var desserialized = JsonConvert.DeserializeObject<List<ProductDto>>(content);
            desserialized.Should().NotBeEmpty();
        }
    }
}
