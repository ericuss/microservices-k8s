namespace Lanre.BFFs.Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Lanre.BFFs.Web.Api.Models;

    [ApiController]
    [Route("Api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private static List<ProductDto> _catalog;
        static CatalogController()
        {
            var random = new Random();
            _catalog = new List<ProductDto>()
                       {
                           new ProductDto(){ Name = "Beer", Stock = random.Next(1,100) },
                           new ProductDto(){ Name = "Fanta", Stock = random.Next(1,100) },
                           new ProductDto(){ Name = "Cocke", Stock = random.Next(1,100) },
                       };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(_catalog.ToList());
        }
    }
}
