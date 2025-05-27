using System.Security.Claims;
using EstudoPlanner.BLL.Services.Auth;
using EstudoPlanner.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _authService.Register(requestDto);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _authService.Login(requestDto);

        return result.Success ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpPut("update-login")]
    public async Task<IActionResult> UpdateLogin([FromBody] LoginUpdateDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(userIdClaim);

        var result = await _authService.UpdateLogin(userId, requestDto);
        return result.Success ? Ok(result) : BadRequest(result);
    }

}