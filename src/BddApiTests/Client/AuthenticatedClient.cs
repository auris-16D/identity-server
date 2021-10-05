using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace BddApiTests.Client
{
    public class AuthenticatedClient
    {
        public AuthenticatedClient()
        {
        }

        public static async Task<HttpClient> Get()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest {
                        Address = "https://identity:5005",
                        Policy =
                        {
                            RequireHttps = false
                        }});
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

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
                return null;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            return apiClient;
        }
    }
}
