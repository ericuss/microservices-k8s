namespace Lanre.BFFs.Web.Api.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Lanre.Bffs.Web.Api.Services;
    using System.Threading.Tasks;
    using Lanre.BFFs.Web.Api.Models;
    using System.Collections.Generic;

    [ApiController]
    [Route("Api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStockService _stockService;

        public CatalogController(IProductService productService, IStockService stockService)
        {
            this._productService = productService;
            this._stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
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
                    Stock = stock?.FirstOrDefault(s => s.Id == p.Id)?.Stock ?? 0,
                };
                catalog.Add(catalogItem);
            });

            return this.Ok(catalog.ToList());
        }
    }
}
