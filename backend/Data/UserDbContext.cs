using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class UserDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}