namespace Lanre.Bffs.Web.Api.Services
{
    using Lanre.Bffs.Web.Api.Infrastructure.Settings;
    using Lanre.Bffs.Web.Api.Services.Core;
    using Lanre.BFFs.Web.Api.Models;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class StockService : ServiceCore, IStockService
    {
        public StockService(IOptions<List<ApiSettings>> apisSettings, ILogger<StockService> logger)
            : base(apisSettings, logger)
        {
        }

        public Task<List<StockDto>> GetStock()
        {
            const string stockApi = "stock";
            const string stockEndpoint = "/api/stock";
            return this.Get<List<StockDto>>(stockApi, stockEndpoint);
        }
    }
}