namespace EstudoPlanner.DTO.Auth;

public class AuthResultDto
{
    public bool Success { get; set; } = true;
    public string? Token { get; set; }
    public List<string>? Errors { get; set; }
}