using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            //get access token
            var identityClient = this._httpClientFactory.CreateClient();
            var discoveryDocument = await identityClient.GetDiscoveryDocumentAsync("https://localhost:44393/");
            var tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "ApiServer",
            };

            var tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(tokenRequest);

            // get secret data
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:44332/secret");
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new 
            {
                access_token = tokenResponse.AccessToken,
                message = content
            });
        }
    }
}
