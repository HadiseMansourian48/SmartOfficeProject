using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartOffice.Domain.Constants;
using SmartOffice.Domain.Entities;
using SmartOffice.Infrastructure.Persistence.Configurations;

namespace SmartOffice.Infrastructure.Persistence.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db, IConfiguration config)
        {
            var existing = await db.Permissions.Select(p => p.Name).ToListAsync();
            foreach (var name in PermissionNames.All().Except(existing))
                db.Permissions.Add(new Permission { Id = Guid.NewGuid(), Name = name, Description = name
                });
            await db.SaveChangesAsync();

            var allPermissions = await db.Permissions.ToListAsync();

            var admin = await db.Roles.Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == RoleConfiguration.AdminRoleId);
            if (admin is null)
            {
                admin = new Role { Id = RoleConfiguration.AdminRoleId, Name = RoleNames.Admin, Description = " مدیر سیستم" };
                db.Roles.Add(admin);
            }
            foreach (var p in allPermissions.Where(p => admin.Permissions.All(x => x.Id != p.Id)))
                admin.Permissions.Add(p);

            var employeeRole = await db.Roles.Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Name == RoleNames.Employee);
            if(employeeRole is null)
            {
                employeeRole = new Role { Id = Guid.NewGuid(), Name = RoleNames.Employee, Description = " کارمند" };
                db.Roles.Add(employeeRole);
            }
            var viewEmployees = allPermissions.First(p => p.Name == PermissionNames.Employees.View);
            if(employeeRole.Permissions.All(p =>p.Id != viewEmployees.Id))
                employeeRole.Permissions.Add(viewEmployees);

            if(!await db.Users.AnyAsync(u => u.UserName == "admin"))
            {
                var password = config["SeedAdmin:Password"] ?? "Admin@12345";
                db.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Email = "admin@smartoffice.local",
                    FullName = "مدیر سیستم",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    RoleId = admin.Id
                });
            }
            await db.SaveChangesAsync();
        }
    }
}
