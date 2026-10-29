using SmartOffice.Application.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore; 
using SmartOffice.Domain.Entities;

namespace SmartOffice.Infrastructure.Persistence.Repositories;
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) =>
            _context = context;

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Role)
                        .Include(u => u.Employee)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                        .Include(u => u.Role)
                        .Include(u => u.Employee)
                        .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        { 
            return await _context.Users
                .Include(u => u.Role)
                    .ThenInclude(r => r.Permissions)
                .Include(u => u.Employee)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

                    
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> RoleExistsAsync(Guid roleId)         
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }

        public async Task<User> AddAsync(User user) 
        {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;

        }

    public async Task<Guid?> GetRoleIdByNameAsync(string roleName)
    {

        return await _context.Roles
            .Where(r => r.Name == roleName)
            .Select(r => (Guid?)r.Id)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> EmployeeExistsAsync(Guid employeeId)
    {
        return await _context.Employees.AnyAsync(e => e.Id == employeeId);
    }
    public async Task<bool> EmployeeLinkedAsync(Guid employeeId)
    {
        return await _context.Users.AnyAsync(u => u.EmployeeId == employeeId);
    } 
}

