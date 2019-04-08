using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
      
        public ActionResult<IEnumerable<string>> Get()
        {
            return new [] { "value1", "value2" };
        }

        [HttpGet("understand-token")]

        public ActionResult<IEnumerable<string>> Understandtoken([FromHeader(Name = "Authorization")] string token)
        {
              const string auth0Domain = "http://localhost:5000"; 
                const string auth0Audience = "api";

            try
                {
               
                IDocumentRetriever r = new HttpDocumentRetriever() { RequireHttps = false };
                    IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"http://localhost:5000/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever(),r);
                    OpenIdConnectConfiguration openIdConfig = AsyncHelper.RunSync(async () => await configurationManager.GetConfigurationAsync(CancellationToken.None));
                TokenValidationParameters validationParameters =
                       new TokenValidationParameters
                       {
                           ValidIssuer = auth0Domain,
                           ValidAudiences = new[] { auth0Audience },
                           IssuerSigningKeys = openIdConfig.SigningKeys
                          
                       };
                    SecurityToken validatedToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
               
                var user = handler.ValidateToken(token, validationParameters, out validatedToken);
                    var check = new List<String>();
                check.Add($"Token is Valid");
                check.Add($" User Id: {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
                
                check.Add($"role of the user is: {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value}");
                return check;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error occurred while validating token: {e.Message}");
                    throw;
                }


        }

        // GET api/values/5
        [Authorize(Roles="AppUser")]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Token you are getting is a valid Token";
        }

       
    }
}
