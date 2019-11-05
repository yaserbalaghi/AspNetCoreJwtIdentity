using CrossCutting.Identity.Jwt.Config;
using CrossCutting.Identity.Jwt.Context;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Entities;
using CrossCutting.Identity.Jwt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Identity.Jwt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtIdentityServices(this IServiceCollection services, IConfiguration configuration,out JwtSettingsContainer jwtSettings)
        {
            var settingsSectionName = "JwtSettings";

            var settings = configuration.GetSection(settingsSectionName).Get<JwtSettingsContainer>();
            services.Configure<JwtSettingsContainer>(configuration.GetSection(settingsSectionName));
            
            services.AddIdentity<ApplicationUser, ApplicationRole>(identityOptions =>
            {
                identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumeric;
                identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;
                identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
            })
                
            .AddEntityFrameworkStores<JwtIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<JwtUserService>();
            services.AddScoped<JwtRoleService>();
            services.AddScoped<JwtSignInService>();
            services.AddScoped<ITokenBuilder, JsonWebTokenBuilder>();
            services.AddDbContext<JwtIdentityDbContext>(options =>
            {
                options.UseSqlServer(settings.ConnectionString);
            });

            jwtSettings = settings;
            return services;
        }
    }
}
