namespace Lanre.Bffs.Web.Api.Services.Core
{
    using Lanre.Bffs.Web.Api.Infrastructure.Settings;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public abstract class ServiceCore
    {
        protected readonly HttpClient httpClient;
        protected readonly IOptions<List<ApiSettings>> apisSettings;
        private readonly ILogger<ServiceCore> logger;

        public ServiceCore(IOptions<List<ApiSettings>> apisSettings, ILogger<ServiceCore> logger)
        {
            this.httpClient = new HttpClient();
            this.apisSettings = apisSettings;
            this.logger = logger;
        }

        protected async Task<T> Get<T>(string apiKey, string endpoint)
        {
            var url = this.apisSettings.Value.FirstOrDefault(x => x.Name == apiKey)?.Url;
            this.logger.LogInformation($"Calling to {url} {endpoint}");
            var response = await this.httpClient.GetAsync(url + endpoint);
            if (!response.IsSuccessStatusCode)
            {
                // todo
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}