namespace Lanre.Products.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Lanre.Products.Api.Models;
    using Microsoft.ApplicationInsights;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;

    [ApiController]
    [Route("Api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<ProductDto> _products = new List<ProductDto>()
        {
            new ProductDto(){ Id = Guid.Parse("5ba0d11e-3545-4a3a-a844-8d8b2fb0ea1d"), Name = "Beer" , Src = "https://cdn.vuetifyjs.com/images/cards/house.jpg" },
            new ProductDto(){ Id = Guid.Parse("2e31d877-95f8-4f2e-989f-34e3741f4947"), Name = "Fanta", Src = "https://cdn.vuetifyjs.com/images/cards/house.jpg" },
            new ProductDto(){ Id = Guid.Parse("8be1a79f-64a9-4513-a44f-35b8c943a5cd"), Name = "Cocke", Src = "https://cdn.vuetifyjs.com/images/cards/house.jpg" },
        };
        private readonly ILogger<ProductsController> _logger;
        private readonly TelemetryClient _telemetry;

        public ProductsController(ILogger<ProductsController> logger, TelemetryClient telemetry)
        {
            this._logger = logger;
            this._telemetry = telemetry;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var response = this.Ok(_products.ToList());

            stopWatch.Stop();
            this._logger.LogError($"products get request elapsed: {stopWatch.Elapsed}");
            this._telemetry.TrackRequest("products get", DateTimeOffset.UtcNow, stopWatch.Elapsed, "200", true);
            this._telemetry.Flush();
            return response;
        }
    }
}
