using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrossCutting.Identity.Jwt.Services
{
    public class JwtUserService : UserManager<ApplicationUser>
    {
        public JwtUserService(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<ApplicationUser> GetAsync(String username, String password)
        {
            var user = await FindByNameAsync(username);

            if (user == null || await CheckPasswordAsync(user, password))
                return null;

            return user;
        }

        public async Task UpdateLastLoginDateAsync(ApplicationUser user)
        {
            user.LastLoginDate = DateTime.Now;
            await UpdateAsync(user);
        }
    }
}