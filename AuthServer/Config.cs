using IdentityServer4.Models;
using System.Collections.Generic;
using GrantTypes = IdentityServer4.Models.GrantTypes;
using IdentityServer4.Test;
using static IdentityServer4.IdentityServerConstants;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;

public class Config
{
    public static List<TestUser> GetUsers()
    {
        return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "0001",
                Username = "sonali",
                Password = "singh",

                Claims = new[]
            {
                new Claim("Employee", "Sonali singh"),
                new Claim("Email", "http://hamidmosalla.com"),
                new Claim("given_name", "sonali"),
                new Claim("family_name", "Underwood"),
                new Claim("address", "Main Road 1"),
                new Claim("subscriptionlevel", "Paying User"),
                new Claim("Country", "India"),
                new Claim("Employee Number", "75024071"),
                new Claim("role","Paying User")
            }

            },
            new TestUser
            {
                SubjectId = "0002",
                Username = "sanchit",
                Password = "kumar",

               Claims = new[]
            {
                new Claim("given_name", "sanchit"),
                new Claim("family_name", "Underwood"),
                new Claim("address", "Main Road 1"),
                new Claim("subscriptionlevel", "Paying User"),
                new Claim("country", "India"),
                new Claim("Employee Number", "75024071"),
                new Claim("role","Free User")
            }
            }
        };
    }


    

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


    public static IEnumerable<Client>GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                AlwaysIncludeUserClaimsInIdToken=true,
                ClientName = "Api client Name", //Image Gallery
                ClientId = "resourceOwner", //imagegalleryclient
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


