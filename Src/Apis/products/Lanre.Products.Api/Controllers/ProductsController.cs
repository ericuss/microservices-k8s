namespace Lanre.Products.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Lanre.Products.Api.Models;

    [ApiController]
    [Route("Api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<ProductDto> _products = new List<ProductDto>()
        {
            new ProductDto(){ Name = "Beer" },
            new ProductDto(){ Name = "Fanta" },
            new ProductDto(){ Name = "Cocke" },
        };

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(_products.ToList());
        }
    }
}
