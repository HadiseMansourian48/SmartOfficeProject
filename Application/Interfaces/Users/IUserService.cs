using SmartOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using SmartOffice.Application.DTOs.Users;

namespace SmartOffice.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<UserDto> CreateAsync(CreateUserRequest request); 
    }
}
