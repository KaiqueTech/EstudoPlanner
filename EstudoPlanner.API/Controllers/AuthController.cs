using EstudoPlanner.BLL.Services.Auth;
using EstudoPlanner.DTO.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EstudoPlanner.API.Controllers;

[Route("api/estudoPlanner/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService  authService)
    {
        _authService = authService;
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        var result = await _authService.Register(requestDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        var result = await _authService.Login(requestDto);
        if (!result.Success)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }
}