using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Budget.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Claims;

namespace Budget.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CallApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            var userId = User.FindFirstValue("sub");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = string.Empty;

            try
            {
                content = await client.GetStringAsync("http://localhost:6001/identity");
                //content = await client.GetStringAsync("http://localhost:6001/budget?principalId=a176ff96-baeb-4dd9-87e8-ed5ad9843c8b");
                //content = await client.GetStringAsync("http://localhost:6001/budget");
            }
            catch (HttpRequestException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
                    var response = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                    {
                        Address = "https://localhost:5005/connect/token",

                        ClientId = "mvc",
                        ClientSecret = "secret",

                        RefreshToken = refreshToken
                    });
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);
                    //content = await client.GetStringAsync("http://localhost:6001/budget?principalId=a176ff96-baeb-4dd9-87e8-ed5ad9843c8b");
                    content = await client.GetStringAsync("http://localhost:6001/identity");
                } 
            }
            
            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet]
        public async Task<string> GetClient()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //    return;
            //}

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",

                Scope = "api1"
            });

            return tokenResponse.AccessToken;
        }
    }
}
