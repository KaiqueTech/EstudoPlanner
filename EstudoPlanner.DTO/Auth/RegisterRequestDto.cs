using System.ComponentModel.DataAnnotations;

namespace EstudoPlanner.DTO.Auth;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid format.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password must be less than 100 characters.")]
    public string Password { get; set; } = string.Empty;
}