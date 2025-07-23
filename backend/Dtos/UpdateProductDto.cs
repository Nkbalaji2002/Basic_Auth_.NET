using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class UpdateProductDto
{
    [Required(ErrorMessage = "Product Id is required.")]
    public int Id { get; set; }

    [StringLength(150, MinimumLength = 2, ErrorMessage = "Product Name must be between 2 and 150 characters.")]
    public string? Name { get; set; }
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    [Range(0.01, 9999999999.99, ErrorMessage = "Price must be greater htan 0.")]
    public decimal? Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    public int? Stock { get; set; }
}