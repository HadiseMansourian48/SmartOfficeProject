using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartOffice.Domain.Entities;


namespace SmartOffice.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder) 
        {
            builder.ToTable("Employees");
            builder.HasKey(e  => e.Id);
            builder.Property(e => e.EmployeeCode)
                .IsRequired();
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.NationalCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.HireDate)
                .IsRequired();

            builder.Property(e => e.EmploymentStatus)
                .IsRequired();

            builder.Property(e => e.DepartmentId)
                .IsRequired();

            builder.Property(e => e.PositionId)
                .IsRequired();
            builder.HasOne<Department>()
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Position>()
                .WithMany()
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
