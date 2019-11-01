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
        public DbSet<UserRoles> UserRoles { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoles>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRoles>().HasOne(ur => ur.User)
                                            .WithMany(u => u.Roles)
                                            .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoles>().HasOne(ur => ur.Role)
                                            .WithMany(u => u.Users)
                                            .HasForeignKey(ur => ur.RoleId);


            SeedInitialData(modelBuilder);
        }

        private static void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = Guid.Parse("08a029db-1b12-4535-863a-f4ad30422219"),
                UserName = "admin",
                Password = "123456",
                FullName = "Yaser Balaghi",
                Gender = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 1,
                Name = "SUPERADMIN",
                Description = "system super administrator"
            }, new Role()
            {
                Id = 2,
                Name = "ADMIN",
                Description = "system administrator"
            }, new Role()
            {
                Id = 3,
                Name = "WRITER",
                Description = "blog writer"
            });

            modelBuilder.Entity<UserRoles>().HasData(new UserRoles()
            {
                UserId = Guid.Parse("08a029db-1b12-4535-863a-f4ad30422219"),
                RoleId = 2
            }, new UserRoles()
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
