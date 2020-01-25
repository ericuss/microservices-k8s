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
            new ProductDto(){ Id = Guid.Parse("5ba0d11e-3545-4a3a-a844-8d8b2fb0ea1d"), Name = "Beer" },
            new ProductDto(){ Id = Guid.Parse("2e31d877-95f8-4f2e-989f-34e3741f4947"), Name = "Fanta" },
            new ProductDto(){ Id = Guid.Parse("8be1a79f-64a9-4513-a44f-35b8c943a5cd"), Name = "Cocke" },
        };

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(_products.ToList());
        }
    }
}
