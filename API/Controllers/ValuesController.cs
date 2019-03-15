using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using static System.Net.WebRequestMethods;

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
            return new string[] { "value1", "value2" };
        }

        //Add the code to repo



        
        [HttpGet("understand-token")]

        public ActionResult<IEnumerable<string>> Understandtoken([FromHeader(Name = "Authorization")] string token)
        {
              const string auth0Domain = "http://localhost:5000"; //Identity Server
                const string auth0Audience = "api"; //Api Identifier

            try
                {
                //Oidc configuration that downloads jwks
                //http so requireshttp =false
                IDocumentRetriever r = new HttpDocumentRetriever() { RequireHttps = false };
                    IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"http://localhost:5000/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever(),r);
                    OpenIdConnectConfiguration openIdConfig = AsyncHelper.RunSync(async () => await configurationManager.GetConfigurationAsync(CancellationToken.None));
                //give token validation parameter 
                TokenValidationParameters validationParameters =
                       new TokenValidationParameters
                       {
                           ValidIssuer = auth0Domain,
                           ValidAudiences = new[] { auth0Audience },
                           IssuerSigningKeys = openIdConfig.SigningKeys
                          
            };
                //Validate the token 
                    SecurityToken validatedToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
               
                var user = handler.ValidateToken(token, validationParameters, out validatedToken);
                    var check = new List<String>();
                //if valid then claims mentioned will be returned
                check.Add($"Token is Valid");
                check.Add($" User Id: {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
                
                check.Add($"role of the user is: {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value}");
                check.Add($"role of the user is: {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value}");
                // check.Add($"Name of the user is: {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.)?.Value}");
                return check;
                }
            //if not valid exception will be thrown
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
            return "Token is a valid token";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
