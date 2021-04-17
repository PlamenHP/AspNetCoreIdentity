// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[] 
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api.read.access"),
                new ApiScope("api.write.access"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("ApiServer", "Api Server")
                { 
                    Scopes = { "api.read.access", "api.write.access"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "spa-client",
                    ClientName = "Spa Client App",
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowedCorsOrigins = {"https://localhost:44301"},
                    RedirectUris = { "https://localhost:44301/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44301/authentication/logout-callback" },

                    AllowedScopes = { "openid", "profile", "api.read.access" },
                    Enabled = true
                }
            };
    }
}