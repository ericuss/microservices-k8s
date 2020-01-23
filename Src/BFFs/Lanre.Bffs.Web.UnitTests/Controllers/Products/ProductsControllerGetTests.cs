namespace Lanre.BFFs.Web.Api.UnitTests.Controllers.BFFs.Web
{
    using Xunit;
    using Lanre.BFFs.Web.Api.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using Lanre.BFFs.Web.Api.Models;
    using Microsoft.AspNetCore.Mvc;

    public class BFFs.WebControllerGetTests
    {
        [Fact]
        public void GetBFFs.WebAndReturnSomeBFFs.Web()
        {
            // Arrange
            var controller = new BFFs.WebController();

            // Act
            var okResult = controller.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var items = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.True(items.Any());
        }
    }
}
