using System.Collections.Generic;
using IdentityServer4.Models;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace AuthServer
{
    public class Config
    {
        public static IEnumerable<ApiResource>GetApiResources()
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


