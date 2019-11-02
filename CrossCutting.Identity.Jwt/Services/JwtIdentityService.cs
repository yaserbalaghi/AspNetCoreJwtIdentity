using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using static CrossCutting.Identity.Jwt.Config.JwtSettingsHandler;

namespace CrossCutting.Identity.Jwt.Services
{
    public class JwtIdentityService : IJwtIdentityService
    {
        private readonly IAdditionalClaims _additionalClaims;

        public JwtIdentityService(IAdditionalClaims additionalClaims = null)
        {
            _additionalClaims = additionalClaims;
        }

        public String GenerateToken(ApplicationUser user)
        {
            var securityKey = Encoding.UTF8.GetBytes(Settings.SecretKey);
            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptKey = Encoding.UTF8.GetBytes(Settings.EncryptKey);
            var encryptingCredentials =
                new EncryptingCredentials(new SymmetricSecurityKey(encryptKey), SecurityAlgorithms.Aes128KW,
                    SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = Settings.Audience,
                Issuer = Settings.Issuer,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(Settings.NotBeforeMinutes),
                Expires = DateTime.Now.AddDays(Settings.ExpirationDay),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials
            };

            var claims = new List<Claim>();
            claims.AddRange(_getDefaultClaims(user));

            if (_additionalClaims != null)
                claims.AddRange(_additionalClaims.GetCustomClaims(user));

            tokenDescriptor.Subject = new ClaimsIdentity(claims);


            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var writeToken = tokenHandler.WriteToken(securityToken);
            return writeToken;
        }
        private IEnumerable<Claim> _getDefaultClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Surname, user.FullName),
                new Claim(ClaimTypes.Email, user.Email ),
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType, user.SecurityStamp)
            };

            claims.AddRange(user.Roles.Select(userRoles =>
                new Claim(ClaimTypes.Role, userRoles.ApplicationRole.Name)));

            return claims;
        }
    }
}