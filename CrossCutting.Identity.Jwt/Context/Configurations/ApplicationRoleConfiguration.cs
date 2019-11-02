using CrossCutting.Identity.Jwt.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrossCutting.Identity.Jwt.Context.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasAlternateKey(r => r.Name);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
        }
    }
}
