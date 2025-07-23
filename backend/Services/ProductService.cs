using backend.Data;
using backend.Dtos;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class ProductService(UserDbContext context) : IProductService
{
    /* Mapping Product -> ProductDto */
    private static ProductResponseDto MapToDto(Product product)
    {
        return new ProductResponseDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await context.Products.ToListAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        return product == null ? null : MapToDto(product);
    }

    public async Task<ProductResponseDto?> CreateAsync(CreateProductDto dto)
    {
        var product = new Product()
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        if (dto.Name != null)
        {
            product.Name = dto.Name;
        }

        if (dto.Description != null)
        {
            product.Description = dto.Description;
        }

        if (dto.Price.HasValue)
        {
            product.Price = dto.Price.Value;
        }

        if (dto.Stock.HasValue)
        {
            product.Stock = dto.Stock.Value;
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }
}