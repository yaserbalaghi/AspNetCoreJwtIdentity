using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Config;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CrossCutting.Identity.Jwt.Services
{
    public class JsonWebTokenBuilder : ITokenBuilder
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettingsContainer _settings;
        private readonly IAdditionalClaims _additionalClaims;

        public JsonWebTokenBuilder(SignInManager<ApplicationUser> signInManager,IOptions<JwtSettingsContainer> settings,IAdditionalClaims additionalClaims = null)
        {
            _signInManager = signInManager;
            _settings = settings.Value;
            _additionalClaims = additionalClaims;
        }
         
        public async Task<String> GenerateTokenAsync(ApplicationUser user)
        {
            var securityKey = Encoding.UTF8.GetBytes(_settings.SecretKey);
            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptKey = Encoding.UTF8.GetBytes(_settings.EncryptKey);
            var encryptingCredentials =
                new EncryptingCredentials(new SymmetricSecurityKey(encryptKey), SecurityAlgorithms.Aes128KW,
                    SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _settings.Audience,
                Issuer = _settings.Issuer,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_settings.NotBeforeMinutes),
                Expires = DateTime.Now.AddDays(_settings.ExpirationDay),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = await _getUserAsSubject(user)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var writeToken = tokenHandler.WriteToken(securityToken);
            return writeToken;
        }

        private async Task<ClaimsIdentity> _getUserAsSubject(ApplicationUser user)
        {
            var claims = new List<Claim>();
            claims.AddRange(await _getDefaultClaims(user));

            if (_additionalClaims != null)
                claims.AddRange(_additionalClaims.GetCustomClaims(user));

            return new ClaimsIdentity(claims);
        }

        private async Task<IEnumerable<Claim>> _getDefaultClaims(ApplicationUser user)
        {
            var result = await _signInManager.ClaimsFactory.CreateAsync(user);
            return result.Claims;
        }
    }
}