using Enterprise.Payroll.Domain.Entities;
using Enterprise.Payroll.Application.DTOs.Auth;

public interface IAuthService
{
    Task RegisterAsync(string username, string email, string password);
    Task<AuthResponse> LoginAsync(string username, string password);
    Task<string> RefreshAccessTokenAsync(string refreshToken);
}
