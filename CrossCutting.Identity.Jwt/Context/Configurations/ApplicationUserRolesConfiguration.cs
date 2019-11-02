using System;
using System.Collections.Generic;
using System.Text;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossCutting.Identity.Jwt.Context.Configurations
{
    public class ApplicationUserRolesConfiguration : IEntityTypeConfiguration<ApplicationUserRoles>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoles> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);

            builder.HasOne(ur => ur.ApplicationRole)
                .WithMany(u => u.Users)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
