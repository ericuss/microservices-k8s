namespace Lanre.BFFs.Web.Api.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Lanre.Bffs.Web.Api.Services;
    using System.Threading.Tasks;
    using Lanre.BFFs.Web.Api.Models;
    using System.Collections.Generic;
    using Microsoft.ApplicationInsights;
    using System;
    using System.Diagnostics;
    using System.Net;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("Api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStockService _stockService;
        private readonly ILogger<CatalogController> _logger;
        private readonly TelemetryClient _telemetry;

        public CatalogController(IProductService productService, IStockService stockService, ILogger<CatalogController> logger, TelemetryClient telemetry)
        {
            this._productService = productService;
            this._stockService = stockService;
            this._logger = logger;
            this._telemetry = telemetry;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            // temp
            var products = await this._productService.GetProducts();
            var stock = await this._stockService.GetStock();
            var catalog = new List<CatalogDto>();
            products.ForEach(p =>
            {
                var catalogItem = new CatalogDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Src = p.Src,
                    Stock = stock?.FirstOrDefault(s => s.Id == p.Id)?.Stock ?? 0,
                };
                catalog.Add(catalogItem);
            });
            stopWatch.Stop();
            this._logger.LogError($"Catalog get request elapsed: {stopWatch.Elapsed}");
            this._telemetry.TrackRequest("Catalog get", DateTimeOffset.UtcNow, stopWatch.Elapsed, "200", true);
            this._telemetry.Flush();
            return this.Ok(catalog);
        }
    }
}
