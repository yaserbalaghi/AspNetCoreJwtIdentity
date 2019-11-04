using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CrossCutting.Identity.Jwt.Config;
using CrossCutting.Identity.Jwt.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CrossCutting.Identity.Jwt.Extensions
{
    public static class AuthenticationBuilderExtension
    {
        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, JwtSettingsContainer jwtSettings)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).SetJwtBearerOptions(jwtSettings);
        }


        private static AuthenticationBuilder SetJwtBearerOptions(this AuthenticationBuilder builder, JwtSettingsContainer jwtSettings)
        {
            return builder.AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireSignedTokens = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.EncryptKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        var claimsIdentity = (ClaimsIdentity)ctx.Principal.Identity;
                        if (claimsIdentity.Claims == null || !claimsIdentity.Claims.Any())
                        {
                            ctx.Fail("token is invalid.");
                            return;
                        }

                        var securityStamp = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "AspNet.Identity.SecurityStamp");
                        if (securityStamp == null)
                        {
                            ctx.Fail("token is invalid.");
                            return;
                        }

                        var userId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        if (userId == null || !Guid.TryParse(userId.Value, out var userIdValue))
                        {
                            ctx.Fail("token is invalid.");
                            return;
                        }

                        var userService = ctx.HttpContext.RequestServices.GetRequiredService<JwtUserService>();
                        var user = await userService.FindByIdAsync(userIdValue.ToString());
                        if (user == null || user.SecurityStamp != securityStamp.Value)
                        {
                            ctx.Fail("token is invalid.");
                            return;
                        }

                        await userService.UpdateLastLoginDateAsync(user);
                    }
                };
            });
        }
    }
}