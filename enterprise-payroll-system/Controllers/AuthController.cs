using Enterprise.Payroll.Application.DTOs.Auth;
using Enterprise.Payroll.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _authService.RegisterAsync(
            request.Username,
            request.Email,
            request.Password);

        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(
            request.Username,
            request.Password);

        return Ok(new { token });
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        var newAccessToken = await _authService.RefreshAccessTokenAsync(request.RefreshToken);
        return Ok(new { accessToken = newAccessToken });
    }

}

public record RegisterRequest(string Username, string Email, string Password);
public record LoginRequest(string Username, string Password);
