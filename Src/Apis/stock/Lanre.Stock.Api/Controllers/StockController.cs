namespace Lanre.Stock.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Lanre.Stock.Api.Models;

    [ApiController]
    [Route("Api/[controller]")]
    public class StockController : ControllerBase
    {
        private static List<ProductDto> _stock;
        static StockController()
        {
            var random = new Random();
            _stock = new List<ProductDto>()
                    {
                        new ProductDto(){ Stock = random.Next(1,100) },
                        new ProductDto(){ Stock = random.Next(1,100) },
                        new ProductDto(){ Stock = random.Next(1,100) },
                    };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(_stock.ToList());
        }
    }
}
