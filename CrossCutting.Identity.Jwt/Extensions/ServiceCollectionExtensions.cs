using System;
using System.Collections.Generic;
using System.Text;
using CrossCutting.Identity.Jwt.Context;
using CrossCutting.Identity.Jwt.Contracts;
using CrossCutting.Identity.Jwt.Repositories;
using CrossCutting.Identity.Jwt.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static CrossCutting.Identity.Jwt.Config.JwtSettingsHandler;

namespace CrossCutting.Identity.Jwt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtIdentityServices(this IServiceCollection services)
        {
            return services.AddScoped<IJwtIdentityService, JwtIdentityService>().
                            AddScoped<IJwtIdentityRepository, JwtIdentityRepository>().
                            AddDbContext<IdentityDbContext>(options =>
                            {
                                options.UseSqlServer(Settings.ConnectionString);
                            });
        }
    }
}
