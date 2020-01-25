namespace Microsoft.Extensions.DependencyInjection
{
    using Lanre.Bffs.Web.Api.Services;

    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            service
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IStockService, StockService>()
                ;
            return service;
        }
    }
}