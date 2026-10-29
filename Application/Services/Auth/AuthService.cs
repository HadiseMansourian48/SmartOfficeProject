using SmartOffice.Application.DTOs.Auth;
using SmartOffice.Application.Interfaces.Auth;
using SmartOffice.Application.Interfaces.Users;
using SmartOffice.Domain.Constants;
using SmartOffice.Domain.Entities;

namespace SmartOffice.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(IUserRepository repository, IJwtTokenGenerator jwt)
    {
        _repository = repository;
        _jwt = jwt;
    }

    public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
    {
        var username = request.Username.Trim();
        var email = request.Email.Trim().ToLowerInvariant();

        if (await _repository.UserNameExistsAsync(username))
            throw new InvalidOperationException("نام کاربری تکراری است.");

        if (await _repository.EmailExistsAsync(email))
            throw new InvalidOperationException("ایمیل تکراری است.");

        var roleId = await _repository.GetRoleIdByNameAsync(RoleNames.Employee)
        ?? throw new InvalidOperationException("نقش پیش‌فرض تعریف نشده است.");
        // ثبت‌نام عمومی همیشه نقش پیش‌فرض Employee می‌گیرد 

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = username,
            Email = email,
            FullName = request.FullName.Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            RoleId = roleId
        };

        await _repository.AddAsync(user);

        var created = await _repository.GetByUserNameAsync(username);
        return BuildResponse(created!);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repository.GetByUserNameAsync(request.UserName.Trim());

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("نام کاربری یا رمز عبور اشتباه است.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("حساب کاربری غیرفعال است.");

        return BuildResponse(user);
    }

    private LoginResponse BuildResponse(User user)
    {
        var (token, expiresAt) = _jwt.Generate(user);

        return new LoginResponse
        {
            Token = token,
            ExpiresAt = expiresAt,
            UserId = user.Id,
            Username = user.UserName,
            FullName = user.FullName,
            RoleName = user.Role.Name,
            Permissions = user.Role.Permissions.Select(p => p.Name).ToList()
        };
    }
}