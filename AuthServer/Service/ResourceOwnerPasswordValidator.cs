using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthServer.Service
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserRepository _rep;

        public ResourceOwnerPasswordValidator(UserRepository rep)
        {
           _rep = rep;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _rep.GetUserOrNull(context.UserName);
            if (user == null) return SetInvalidValidationInContext(context);
            if (context.Password != user.Password) return SetInvalidValidationInContext(context);
            if (user.UserType != "AppUser") return SetInvalidValidationInContext(context);
                    
            context.Result = new GrantValidationResult(context.UserName, "password",null, "local", null);
            return Task.FromResult(context.Result);
        }

        private  static Task SetInvalidValidationInContext(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "authentication failed", null);
            return Task.FromResult(context.Result);
        }
    }
}
