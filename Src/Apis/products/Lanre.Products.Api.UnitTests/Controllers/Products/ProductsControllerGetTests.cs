namespace Lanre.Products.Api.UnitTests.Controllers.Products
{
    using Xunit;
    using Lanre.Products.Api.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using Lanre.Products.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.ApplicationInsights;
    using Moq;
    using Microsoft.Extensions.Logging;
    using System;

    public class ProductsControllerGetTests
    {
        private readonly Mock<ILogger<ProductsController>> _logger;
        private readonly TelemetryClient _telemetry;
        private readonly ProductsController _controller;

        [Obsolete]
        public ProductsControllerGetTests()
        {
            this._logger = new Mock<ILogger<ProductsController>>();
            this._telemetry = new TelemetryClient();

            //this._logger.Setup(x => x.LogInformation(It.IsAny<string>()));

            this._controller = new ProductsController(this._logger.Object, this._telemetry);
        }
        [Fact]
        public void GetProductsAndReturnSomeProducts()
        {
            // Act
            var okResult = this._controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var items = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.True(items.Any());
        }
    }
}
