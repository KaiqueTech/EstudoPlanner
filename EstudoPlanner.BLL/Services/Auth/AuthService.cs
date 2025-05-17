using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstudoPlanner.DAL.DataContext;
using EstudoPlanner.Domain.Models;
using EstudoPlanner.DTO.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EstudoPlanner.BLL.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtTokenGenerateService _jwtTokenGenerateService;

    public AuthService(AppDbContext context, JwtTokenGenerateService jwtTokenGenerateService)
    {
        _context = context;
        _jwtTokenGenerateService = jwtTokenGenerateService;
    }
    
    public async Task<AuthResultDto> Login(LoginRequestDto request)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new AuthResultDto
                {
                    Success = false,
                    Errors = new List<string> { "Invalid Email or Password" }
                };
            }

            var token = _jwtTokenGenerateService.GenerateToken(user);
            return new AuthResultDto
            {
                Success = true,
                Token = token
            };
        }
        catch (Exception ex)
        {
            return new AuthResultDto
            {
                Success = false,
                Errors = new List<string> { $"An error occurred during login: {ex.Message}" }
            };
        }
    }

    public async Task<AuthResultDto> Register(RegisterRequestDto request)
    {
        try
        {
            if (await _context.Users.AnyAsync(email => email.Email == request.Email))
            {
                return new AuthResultDto
                {
                    Success = false,
                    Errors = new List<string> { "Email Already Exists" }
                };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new UserModel
            {
                IdUser = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = hashedPassword
            };

            _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _jwtTokenGenerateService.GenerateToken(user);
            return new AuthResultDto
            {
                Success = true,
                Token = token
            };
        }
        catch(Exception ex)
        {
            return new AuthResultDto
            {
                Success = false,
                Errors = new List<string> { $"An error occurred during login: {ex.Message}" }
            };
        }
    }
}