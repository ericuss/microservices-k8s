using Lanre.BFFs.Web.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lanre.Bffs.Web.Api.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProducts();
    }
}