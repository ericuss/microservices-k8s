namespace SampleClientConsole
{
    using IdentityModel.Client;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Linq;
    using Cocona;

    class Program
    {
        static void Main(string[] args)
        {
            // Cocona parses command-line and executes a command.
            CoconaApp.Run<Program>(args);
        }

        // public method as a command ™
        public async Task TestConnection(string urlIdentity, string urlStock)
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            //var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            var disco = await client.GetDiscoveryDocumentAsync(urlIdentity);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",

                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            //var response = await apiClient.GetAsync("http://localhost:5001/identity");
            var response = await apiClient.GetAsync($"{urlStock}/api/stock");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
