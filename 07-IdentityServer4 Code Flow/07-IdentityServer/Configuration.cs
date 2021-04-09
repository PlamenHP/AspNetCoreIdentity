using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants.StandardScopes;

namespace _07_IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "apione_id",
                    ClientName = "ApiOne",
                    ClientSecrets = { new Secret("apione_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44380/signin-oidc" },
                    AllowedScopes = { "ApiTwo", OpenId, Profile},
                },
                new Client
                {
                    ClientId = "apitwo_id",
                    ClientName = "ApiTwo",
                    ClientSecrets = { new Secret("apitwo_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44396/signin-oidc" },
                    AllowedScopes = { "ApiOne", OpenId, Profile},
                },
                new Client
                {
                    ClientId = "mvc_id",
                    ClientName = "MVC",
                    ClientSecrets = { new Secret("mvc_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44358/signin-oidc" },
                    AllowedScopes = { "ApiOne", "ApiTwo", OpenId, Profile},
                }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("ApiOne"),
                new ApiScope("ApiTwo"),
                new ApiScope("MVC")
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiOne"),
                new ApiResource("ApiTwo"),
                new ApiResource("MVC")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
