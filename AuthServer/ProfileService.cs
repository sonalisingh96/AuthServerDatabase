using AuthServer.Models;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer
{
    public class ProfileService : IProfileService
    {
        // private readonly IAuthRepository _repository;
        private readonly UserRepository _repository;
        //public ProfileService(IAuthRepository rep)
        public ProfileService(UserRepository rep)
        {
            this._repository = rep;
        }
        //public Task GetProfileDataAsync(ProfileDataRequestContext context)
        //{
        //    context.IssuedClaims.AddRange(context.Subject.Claims);

        //    return Task.FromResult(0);
        //}

        //public Task IsActiveAsync(IsActiveContext context)
        //{
        //    return Task.FromResult(0);
        //}



        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                

                var subjectId = context.Subject.GetSubjectId();
                var user = _repository.GetUserByUsernameOrNull(subjectId);
                // = _repository.GetUserById(subjectId);
                var claims = new List<Claim>
                {
                    new Claim("Usertype", user.UserType),
                    new Claim("Username",user.Username),
                    new Claim("Id",user.Id.ToString()),
                    


                };
                if (user.UserType=="AppUser")
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, "AppUser"));
                }
                else
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, "WebUser"));
                }
               
                context.IssuedClaims = claims;
                return Task.FromResult(0);
            }
            catch (Exception x)
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //var user = _repository.GetUserByUsernameOrNull((context.Subject.GetSubjectId()));
            ////var user = _repository.GetUserById(context.Subject.GetSubjectId());
            ////context.IsActive = (user != null) && user.Active;
            return Task.FromResult(0);
        }
    }
}
