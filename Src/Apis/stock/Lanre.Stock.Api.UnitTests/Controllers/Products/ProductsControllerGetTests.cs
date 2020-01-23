namespace Lanre.Stock.Api.UnitTests.Controllers.Stock
{
    using Xunit;
    using Lanre.Stock.Api.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using Lanre.Stock.Api.Models;
    using Microsoft.AspNetCore.Mvc;

    public class StockControllerGetTests
    {
        [Fact]
        public void GetStockAndReturnSomeStock()
        {
            // Arrange
            var controller = new StockController();

            // Act
            var okResult = controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var items = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.True(items.Any());
        }
    }
}
