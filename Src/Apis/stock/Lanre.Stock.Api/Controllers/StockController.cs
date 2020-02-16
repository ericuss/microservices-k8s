namespace Lanre.Stock.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Lanre.Stock.Api.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize]
    [Route("Api/[controller]")]
    public class StockController : ControllerBase
    {
        private static List<ProductDto> _stock;
        static StockController()
        {
            var random = new Random();
            _stock = new List<ProductDto>()
                    {
                        new ProductDto(){ Id = Guid.Parse("5ba0d11e-3545-4a3a-a844-8d8b2fb0ea1d"), Stock = random.Next(1,100) },
                        new ProductDto(){ Id = Guid.Parse("2e31d877-95f8-4f2e-989f-34e3741f4947"), Stock = random.Next(1,100) },
                        new ProductDto(){ Id = Guid.Parse("8be1a79f-64a9-4513-a44f-35b8c943a5cd"), Stock = random.Next(1,100) },
                    };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(_stock.ToList());
        }
    }
}
