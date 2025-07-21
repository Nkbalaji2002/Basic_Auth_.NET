using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}