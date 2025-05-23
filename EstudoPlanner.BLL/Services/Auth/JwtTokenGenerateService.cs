using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstudoPlanner.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EstudoPlanner.BLL.Services.Auth;

public class JwtTokenGenerateService
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerateService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(UserModel user)
    {
        // var t = _configuration["Jwt:Key"];
        // Console.WriteLine($"key used to generate token: {t}");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:secretKey"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            Issuer = "EstudoPlannerAPI",
            Audience = "EstudoPlannerUsers",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}