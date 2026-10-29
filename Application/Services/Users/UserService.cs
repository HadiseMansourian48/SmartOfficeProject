using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SmartOffice.Application.DTOs.Users;
using SmartOffice.Application.Interfaces.Users;
using SmartOffice.Domain.Entities;

namespace SmartOffice.Application.Services.Users;
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) => _repository = repository;

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(MapToDto).ToList();
        }
        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user is null ? null : MapToDto(user);
        }

    public async Task<UserDto> CreateAsync(CreateUserRequest request)
        {

            var userName = request.UserName.Trim();
            var email = request.Email.Trim().ToLowerInvariant();

            if (await _repository.UserNameExistsAsync(userName))
                throw new InvalidOperationException("نام کاربر تکراری است.");

            if (await _repository.EmailExistsAsync(email))
                throw new InvalidOperationException("ایمیل تکراری است.");

            if (!await _repository.RoleExistsAsync(request.RoleId))
                throw new InvalidOperationException("نقش انتخاب شده وجود ندارد.");

            if(request.EmployeeId is { } employeeId)
            {
            if (!await _repository.EmployeeExistsAsync(employeeId))
                throw new InvalidOperationException("کارمند انتخاب شده وجود ندارد.");
            if (await _repository.EmployeeLinkedAsync(employeeId))
                throw new InvalidOperationException("این کارمند قبلا به کاربر دیگری متصل شده است.");


        }

        var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email,
                FullName = request.FullName.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.RoleId,
                EmployeeId = request.EmployeeId,
            };
            await _repository.AddAsync(user); 

            var created = await _repository.GetByIdAsync(user.Id);
            return MapToDto(created!);
        }

        public async Task<UserDto> LoginAsync(LoginRequest request)
        {
            var user = await _repository.GetByUserNameAsync(request.UserName);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("نام کاربری یا رمز عبور اشتباه است.");

            if (!user.IsActive)
                throw new InvalidOperationException("حساب کاربری غیرفعال است.");

            return MapToDto(user);
        }

        
        

        private static UserDto MapToDto(User u) => new()
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            FullName = u.FullName,
            IsActive = u.IsActive,
            RoleId = u.RoleId,
            RoleName = u.Role.Name,
            EmployeeId = u.EmployeeId,
            EmployeeFullName = u.Employee is null ? null : $"{u.Employee.FirstName} {u.Employee.LastName}"

        };
    } 