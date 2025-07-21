using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

[Table("my_users")]
[Index(nameof(Email), Name = "Index_Email", IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public required string Email { get; set; }

    [Required]
    public required byte[] PasswordHash { get; set; }
    
    [Required]
    public required byte[] PasswordSalt { get; set; }
}