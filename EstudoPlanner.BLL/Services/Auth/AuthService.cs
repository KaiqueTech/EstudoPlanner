using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EstudoPlanner.BLL.Mappings;
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
    private readonly IMapper _mapping;

    public AuthService(AppDbContext context, JwtTokenGenerateService jwtTokenGenerateService, IMapper mapping)
    {
        _context = context;
        _jwtTokenGenerateService = jwtTokenGenerateService;
        _mapping = mapping;
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
            var response = _mapping.Map<LoginResponseDto>(user);
            response.Token = token;
            return new AuthResultDto
            {
                Success = true,
                LoginResponse = response
            };
        }
        catch (Exception ex)
        {
            return new AuthResultDto
            {
                Success = false,
                Errors = new List<string> { $"An error occurred during registration: {ex.Message}" }
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

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _jwtTokenGenerateService.GenerateToken(user);
            
            var response = _mapping.Map<LoginResponseDto>(user);
            response.Token = token;
            
            return new AuthResultDto
            {
                Success = true,
                LoginResponse = response
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

    public async Task<AuthResultDto> UpdateLogin(Guid userId, LoginUpdateDto request)
    {
        try
        {
            var login = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == userId);
            if (login == null)
            {
                return new AuthResultDto
                {
                    Success = false,
                    Errors = new List<string> { $"Not found user" }
                };
            }

            if (!string.IsNullOrWhiteSpace(request.UserName) && request.UserName != login.Name)
            {
                login.Name = request.UserName;
            }

            if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != login.Email)
            {
                var existinEmail = await _context.Users.AnyAsync(x => x.Email == login.Email && x.IdUser != userId);
                return new AuthResultDto
                {
                    Success = false,
                    Errors = new List<string> { $"Email is already in use" }
                };
            }

            login.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                login.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

             _context.Users.Update(login);
             await _context.SaveChangesAsync();

             var response =  _mapping.Map<LoginResponseDto>(login);
             response.Token = _jwtTokenGenerateService.GenerateToken(login);

             return new AuthResultDto
             {
                 Success = true,
                 LoginResponse = response
             };

        }
        catch (Exception ex)
        {
            return new AuthResultDto
            {
                Success = false,
                Errors = new List<string> { $"An error ocurred while updating the data{ex.Message}" }
            };
        }
    }
}