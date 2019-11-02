using System;
using System.Collections.Generic;
using System.Text;
using CrossCutting.Identity.Jwt.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossCutting.Identity.Jwt.Context.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasAlternateKey(u => u.UserName);
            builder.HasAlternateKey(u => u.Email);

            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Password).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.Property(u => u.FullName).HasMaxLength(100);
        }
    }
}
