using SmartOffice.Application.DTOs.Auth;

namespace SmartOffice.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}