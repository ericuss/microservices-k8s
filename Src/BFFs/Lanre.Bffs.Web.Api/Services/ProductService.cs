namespace Lanre.Bffs.Web.Api.Services
{
    using Lanre.Bffs.Web.Api.Infrastructure.Settings;
    using Lanre.Bffs.Web.Api.Services.Core;
    using Lanre.BFFs.Web.Api.Models;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : ServiceCore, IProductService
    {
        public ProductService(IOptions<List<ApiSettings>> apisSettings, ILogger<ProductService> logger)
            : base(apisSettings, logger)
        {
        }

        public Task<List<ProductDto>> GetProducts()
        {
            const string productApi = "products";
            const string productsEndpoint = "/api/products";
            return this.Get<List<ProductDto>>(productApi, productsEndpoint);
        }
    }
}