namespace Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IdentityModel.Client;

    class Program
    {
        static async Task Main(string[] args)
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5005");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
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
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:6001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"IdentityController error code: {response.StatusCode}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"IdentityController response: {content}");
            }
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            response = await apiClient.GetAsync("http://localhost:6001/weatherforecast");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"WeatherForecastController error code: {response.StatusCode}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"WeatherForecastController response: {content}");
            }

            // request token
            tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "TrekClient",
                ClientSecret = "secret",

                Scope = "trekApi"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            //var apiClient = new HttpClient();
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            //var response1 = await apiClient.GetAsync("https://localhost:5003/trek");
            //if (!response1.IsSuccessStatusCode)
            //{
            //    Console.WriteLine($"TreksController response: {response1.StatusCode}");
            //}
            //else
            //{
            //    var content = await response1.Content.ReadAsStringAsync();
            //    Console.WriteLine($"TreksController response: {content}");
            //}
        }
    }
}
