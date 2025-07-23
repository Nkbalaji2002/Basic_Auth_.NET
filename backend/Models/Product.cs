using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product Name is required.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Product Name must be between 2 and 150 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(100, ErrorMessage = "Description cannot exceed 500 characters.")]
    public required string Description { get; set; }

    [Column(TypeName = "decimal(12,2")]
    [Range(0.01, 9999999999.99, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    public int Stock { get; set; }
}