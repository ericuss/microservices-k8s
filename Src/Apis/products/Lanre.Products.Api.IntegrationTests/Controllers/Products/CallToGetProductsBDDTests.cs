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
    using TestStack.BDDfy;

    [Story(
       AsA = "As a user",
       IWant = "I want to get products",
       SoThat = "So that I can get products")]
    public class CallToGetProductsTests : IClassFixture<WebHostFixture>, IDisposable
    {
        private readonly WebHostFixture _fixture;
        private readonly HttpClient _client;
        private HttpResponseMessage _response;

        public CallToGetProductsTests(WebHostFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
        }

        public void Dispose() => _fixture.Output = null;

        [Fact]
        public void CanGetProducts()
        {
            this.Given(s => s.GivenACall())
                .When(s => s.WhenICallToProcuts())
                .Then(s => s.ResponseIsSuccess())
                    .And(s => s.AndGetSomeProducts())
                .BDDfy();
        }

        private void GivenACall() { }

        private async Task WhenICallToProcuts()
        {
            this._response = await _client.GetAsync("/api/products");
        }

        private void ResponseIsSuccess()
        {
            this._response.EnsureSuccessStatusCode();
        }

        private async Task AndGetSomeProducts()
        {
            var content = await this._response.Content.ReadAsStringAsync();
            var desserialized = JsonConvert.DeserializeObject<List<ProductDto>>(content);
            desserialized.Should().NotBeEmpty();
        }
    }
}
