using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class CreateProductDto
{
    [Required(ErrorMessage = "Product Name is required.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Product Name must be between 2 and 150 characters.")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
    public required string Description { get; set; }
    
    [Range(0.01, 9999999999.99, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Stock can't be negative.")]
    public int Stock { get; set; }
}