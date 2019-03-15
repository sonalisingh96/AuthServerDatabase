using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{
    //to authenticate and give access token
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //  private  readonly IAuthRepository _rep;
        private readonly UserRepository _rep;

        // public ResourceOwnerPasswordValidator(IAuthRepository rep)
        public ResourceOwnerPasswordValidator(UserRepository rep)
        {
            this._rep = rep;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _rep.GetUserByUsernameOrNull(context.UserName);
            if (user == null) return SetInvalidValidationInContext(context);
            if (context.Password != user.Password) return SetInvalidValidationInContext(context);
            if (user.UserType != "AppUser") return SetInvalidValidationInContext(context);
                    
            context.Result = new GrantValidationResult(context.UserName, "password", null, "local", null);
            return Task.FromResult(context.Result);
        }

        private  static Task SetInvalidValidationInContext(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "authencation failed", null);
            return Task.FromResult(context.Result);
        }


    }
}
