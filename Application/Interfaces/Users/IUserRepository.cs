using System;
using System.Collections.Generic;
using System.Text;
using SmartOffice.Domain.Entities;

namespace SmartOffice.Application.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync ();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByUserNameAsync(string userName);
        Task<bool> UserNameExistsAsync(string useName);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> RoleExistsAsync(Guid roleId);
        Task<User> AddAsync(User user);
        Task<bool> EmployeeExistsAsync(Guid employeeId);
        Task<bool> EmployeeLinkedAsync(Guid employeeId);
        Task<Guid?> GetRoleIdByNameAsync(string roleName);

    }
}
