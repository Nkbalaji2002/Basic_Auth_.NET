using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class RegisterationDto
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email addresss format.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).[8,]$")]
    public required string Password { get; set; }
}