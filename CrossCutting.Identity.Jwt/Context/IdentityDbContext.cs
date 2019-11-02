using System;
using System.Collections.Generic;
using System.Text;
using CrossCutting.Identity.Jwt.Config;
using CrossCutting.Identity.Jwt.Context.Configurations;
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

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRoles> UserRoles { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationUserRolesConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

            SeedInitialData(modelBuilder);
        }

        private static void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = Guid.Parse("08a029db-1b12-4535-863a-f4ad30422219"),
                UserName = "admin",
                Password = "123456",
                FullName = "Yaser Balaghi",
                Email = "Eng.balaghi@yahoo.com",
                SecurityStamp = Guid.NewGuid().ToString()
            });

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole()
            {
                Id = 1,
                Name = "SUPERADMIN",
                Description = "system super administrator"
            }, new ApplicationRole()
            {
                Id = 2,
                Name = "ADMIN",
                Description = "system administrator"
            }, new ApplicationRole()
            {
                Id = 3,
                Name = "WRITER",
                Description = "blog writer"
            });

            modelBuilder.Entity<ApplicationUserRoles>().HasData(new ApplicationUserRoles()
            {
                UserId = Guid.Parse("08a029db-1b12-4535-863a-f4ad30422219"),
                RoleId = 2
            }, new ApplicationUserRoles()
            {
                UserId = Guid.Parse("08a029db-1b12-4535-863a-f4ad30422219"),
                RoleId = 3
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
