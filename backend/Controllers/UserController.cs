using backend.Data;
using backend.Dtos;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController(UserDbContext context) : ControllerBase
{
    /* POST : api/v1/user/register */
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterationDto dto)
    {
        /* Validate the incoming model */
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        /* Check if the email already exists*/
        if (await context.Users.AnyAsync(user => user.Email == dto.Email))
        {
            return BadRequest("Email is already in use.");
        }

        /* Create password hash and salt */
        PasswordHasher.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        /* Create user entity */
        var user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        /* Add user to the database */
        context.Users.Add(user);
        await context.SaveChangesAsync();

        /* For security reasons, do not return password hash and salt */
        return Ok(new { Message = "User registered successfully." });
    }

    /* POST : api/v1/user/login */
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        /* Validate the incoming model */
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        /* Retrieve the user by email */
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        /* Verify the password */
        if (!PasswordHasher.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Unauthorized("Invalid email or password.");
        }

        /* Generate JWT or other token as needed */
        return Ok(new { Message = "User logged in successfully." });
    }
}