using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SpaClient.Http
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[]
                { 
                    "https://localhost:44300", 
                    "https://localhost:44301", 
                    "https://localhost:44302"
                },
                scopes: new[] { "api.read.access" });
        }
    }
}
