using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CrossCutting.Identity.Jwt.Context;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Repositories;
using CrossCutting.Identity.Jwt.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using static CrossCutting.Identity.Jwt.Config.JwtSettingsHandler;

namespace CrossCutting.Identity.Jwt.Extensions
{
    public static class AuthenticationBuilderExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IJwtIdentityService, JwtIdentityService>();
            services.AddScoped<IJwtIdentityRepository, JwtIdentityRepository>();
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(Settings.ConnectionString);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).SetJwtBearerOptions();
        }

        private static AuthenticationBuilder SetJwtBearerOptions(this AuthenticationBuilder builder)
        {
            return builder.AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireSignedTokens = true,
                    ValidateAudience = true,
                    ValidAudience = Settings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = Settings.Issuer,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SecretKey)),
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.EncryptKey))
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
                        if (userId == null)
                        {
                            ctx.Fail("token is invalid.");
                            return;
                        }

                        var repository = ctx.HttpContext.RequestServices.GetRequiredService<IJwtIdentityRepository>();
                        var user = await repository.Get(Guid.Parse(userId.Value), ctx.HttpContext.RequestAborted);
                        if (user.SecurityStamp != securityStamp?.Value)
                        {
                            ctx.Fail("token is invalid.");
                        }

                        user.LastLoginDate = DateTime.Now;
                        await repository.UpdateAsync(user,ctx.HttpContext.RequestAborted);
                    }
                };
            });
        }
    }
}
