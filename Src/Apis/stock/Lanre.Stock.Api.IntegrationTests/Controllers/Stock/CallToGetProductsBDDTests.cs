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
    using TestStack.BDDfy;

    [Story(
       AsA = "As a user",
       IWant = "I want to get stock",
       SoThat = "So that I can get the stock")]
    public class CallToGetStockBDDTests : IClassFixture<WebHostFixture>, IDisposable
    {
        private readonly WebHostFixture _fixture;
        private readonly HttpClient _client;
        private HttpResponseMessage _response;

        public CallToGetStockBDDTests(WebHostFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
        }

        public void Dispose() => _fixture.Output = null;

        [Fact]
        public void CanGetStock()
        {
            this.Given(s => s.GivenACall())
                .When(s => s.WhenICallToStock())
                .Then(s => s.ResponseIsSuccess())
                    .And(s => s.AndGetSomeProductsOfStock())
                .BDDfy();
        }

        private void GivenACall() { }

        private async Task WhenICallToStock()
        {
            this._response = await _client.GetAsync("/api/stock");
        }

        private void ResponseIsSuccess()
        {
            this._response.EnsureSuccessStatusCode();
        }

        private async Task AndGetSomeProductsOfStock()
        {
            var content = await this._response.Content.ReadAsStringAsync();
            var desserialized = JsonConvert.DeserializeObject<List<ProductDto>>(content);
            desserialized.Should().NotBeEmpty();
        }
    }
}
