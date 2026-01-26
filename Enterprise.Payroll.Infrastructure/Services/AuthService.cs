using Enterprise.Payroll.Application.Interfaces;
using Enterprise.Payroll.Application.DTOs.Auth;
using Enterprise.Payroll.Domain.Entities;
using Enterprise.Payroll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;


public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task RegisterAsync(string username, string email, string password)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AuthResponse> LoginAsync(string username, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == username && x.IsActive);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        var refreshToken = new RefreshToken
        {
            Token = GenerateRefreshToken(),
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
    public async Task<string> RefreshAccessTokenAsync(string refreshToken)
    {
        var storedToken = await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token == refreshToken &&
                !x.IsRevoked &&
                x.ExpiresAt > DateTime.UtcNow);

        if (storedToken == null)
            throw new Exception("Invalid refresh token");

        var user = storedToken.User;

        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var newToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(newToken);
    }

}
