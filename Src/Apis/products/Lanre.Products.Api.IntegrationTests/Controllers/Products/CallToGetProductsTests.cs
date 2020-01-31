namespace Lanre.Products.Api.IntegrationTests.Controllers.Products
{
    using Lanre.Products.Api.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;
    using FluentAssertions;
    using Lanre.Products.Api.IntegrationTests.Core;

    public class CallToGetProductsTests : TestCoreCrud
    {
        public CallToGetProductsTests(WebHostFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [Fact]
        public async Task CallToGetProductsAndReturnOk()
        {
            var result = await Client.GetAsync("/api/products");

            result.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CallToGetProductsAndReturnProducts()
        {
            var desserialized = await this.Get<List<ProductDto>>("/api/products");
            desserialized.Should().NotBeEmpty();
        }
    }
}
