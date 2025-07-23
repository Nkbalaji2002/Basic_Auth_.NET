using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Seed User Data */
        modelBuilder.Entity<UserTwo>().HasData(
            new UserTwo
            {
                Id = 1,
                FullName = "Pranaya Rout",
                Email = "pranaya.rout@example.com",
                PasswordHash = PasswordHasherTwo.HashPassword("Pranaya@123"),
                Role = "Administrator,Manager",
            },
            new UserTwo
            {
                Id = 2,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PasswordHash = PasswordHasherTwo.HashPassword("Pranaya@123"),
                Role = "Administrator",
            },
            new UserTwo
            {
                Id = 3,
                FullName = "Jane Smith",
                Email = "jane.smith@example.com",
                PasswordHash = PasswordHasherTwo.HashPassword("Jane@123"),
                Role = "Manager",
            }
        );

        /* Seed Product Data */
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High performance laptop",
                Price = 1200.00m,
                Stock = 15
            },
            new Product
            {
                Id = 2,
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse",
                Price = 25.99m,
                Stock = 100
            },
            new Product
            {
                Id = 3,
                Name = "Mechanical Keyboard",
                Description = "RGB backlight mechanical keyboard",
                Price = 79.99m,
                Stock = 45
            }
        );
    }

    public DbSet<UserTwo> Users2 { get; set; }
    public DbSet<Product> Products { get; set; }
}