namespace EstudoPlanner.DTO.Auth;

public class AuthResultDto
{
    public bool Success { get; set; } = true;
    public LoginResponseDto LoginResponse { get; set; }
    public List<string>? Errors { get; set; }
}