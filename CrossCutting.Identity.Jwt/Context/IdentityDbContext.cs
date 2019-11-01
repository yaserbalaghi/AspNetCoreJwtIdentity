using System;
using System.Collections.Generic;
using System.Text;
using CrossCutting.Identity.Jwt.Config;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrossCutting.Identity.Jwt.Context
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoleses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoles>().HasKey(ur => new {ur.UserId, ur.RoleId});
            
            modelBuilder.Entity<UserRoles>().HasOne(ur => ur.User)
                                            .WithMany(u => u.Roles)
                                            .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoles>().HasOne(ur => ur.Role)
                                            .WithMany(u => u.Users)
                                            .HasForeignKey(ur => ur.RoleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
