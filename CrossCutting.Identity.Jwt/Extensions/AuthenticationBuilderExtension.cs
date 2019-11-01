using System;
using System.Text;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using static CrossCutting.Identity.Jwt.Config.JwtSettingsHandler;

namespace CrossCutting.Identity.Jwt.Extensions
{
    public static class AuthenticationBuilderExtension 
    {
        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).SetJwtBearerOptions();
        }

        public static AuthenticationBuilder SetJwtBearerOptions(this AuthenticationBuilder builder)
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
            });
        }
    }
}
