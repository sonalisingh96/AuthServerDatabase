using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace AuthServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My API",
                    new List<string>() {"role" , "given_name", "family_name","address", "subscriptionlevel" , "Country", "Employee Number" } )
                {
                    ApiSecrets = { new Secret("apisecret".Sha256())}

                }


            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api"

                    },

                    AllowOfflineAccess = true
                },
                new Client
                {
                    AlwaysIncludeUserClaimsInIdToken=true,
                    ClientName = "Api client Name",
                    ClientId = "resourceOwner", 
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 120,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    AllowedScopes =
                    {
                        "api"
                    },

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    ClientClaimsPrefix = "",
                    AlwaysSendClientClaims=true

                }
            };
        }
    }
}


