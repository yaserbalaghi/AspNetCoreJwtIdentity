using System;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using CrossCutting.Identity.Jwt.Config;
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
                    OnChallenge = ctx =>
                    {
                        if (ctx.AuthenticateFailure != null)
                            return Task.FromException(new UnauthorizedAccessException("Unauthorized Request",
                                    ctx.AuthenticateFailure));
                        return Task.FromException(new UnauthorizedAccessException("Unauthorized Request"));
                    },
                    OnTokenValidated = ctx =>
                    {

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
