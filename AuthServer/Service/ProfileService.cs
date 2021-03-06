﻿using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Service
{
    public class ProfileService : IProfileService
    {
        private readonly UserRepository _repository;

        public ProfileService(UserRepository rep)
        {
           _repository = rep;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var username = context.Subject.GetSubjectId();
            var user = _repository.GetUserOrNull(username);

            var claims = new List<Claim>
            {
                new Claim("UserType", user.UserType),
                new Claim("Username", user.Username),
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtClaimTypes.Role, user.UserType)
            };

            context.IssuedClaims = claims;
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}
