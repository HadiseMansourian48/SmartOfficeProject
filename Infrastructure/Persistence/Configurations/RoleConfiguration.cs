using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {

        public static readonly Guid AdminRoleId =
            Guid.Parse("11111111-1111-1111-1111-111111111111");
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);  
            builder.Property(x => x.Description).IsRequired().HasMaxLength(250);

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                .UsingEntity(j => j.ToTable("RolePermissions"));

            builder.HasData(new Role
            {
                Id = AdminRoleId,
                Name = "Admin",
                Description = "مدیر سیستم"
            });
        }
    }
}
