using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace _06_IS4_ClientCredentials
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiServer"),
                new ApiResource("ApiServer1"),
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("ApiServer"),
                new ApiScope("ApiServer1"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ApiServer", "ApiServer1" },
                }
            };
        }
    }
}
