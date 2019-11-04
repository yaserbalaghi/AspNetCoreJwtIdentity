using System;
using CrossCutting.Identity.Jwt.Context.Configurations;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrossCutting.Identity.Jwt.Context
{
    public class JwtIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public JwtIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
