using EstudoPlanner.DTO.Auth;

namespace EstudoPlanner.BLL.Services.Auth;

public interface IAuthService
{
    Task<AuthResultDto> Login(LoginRequestDto request);
    Task<AuthResultDto> Register(RegisterRequestDto request);
}