using System.Security.Cryptography; // For cryptographic operations like hashing
using System.Text; // For string encoding
using API.Data; // Namespace for application-specific data context
using API.DTOs; // Namespace for Data Transfer Objects
using API.Entities; // Namespace for application-specific entities
using API.Interfaces;
using Microsoft.AspNetCore.Mvc; // For building API controllers
using Microsoft.EntityFrameworkCore; // For Entity Framework Core functionalities

namespace API.Controllers;

// AccountController inherits from BaseApiController
// Handles user account-related operations
public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
  // HTTP POST endpoint for user registration
  [HttpPost("register")] // Endpoint: account/register
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
    // Check if the username already exists
    if (await UserExists(registerDto.Username)) return BadRequest("Username already taken");

    // Create a new HMACSHA512 instance for hashing the password
    using var hmac = new HMACSHA512();

    // Create a new user object with hashed password and salt
    var user = new AppUser
    {
      UserName = registerDto.Username.ToLower(), // Store username in lowercase for consistency
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), // Hash the password
      PasswordSalt = hmac.Key // Store the generated salt
    };

    // Add the user to the database
    context.Users.Add(user);
    await context.SaveChangesAsync(); // Save changes to the database

    return new UserDto
    {
      Username = user.UserName,
      Token = tokenService.CreateToken(user),
    }; // Return the newly created user object
  }

  // HTTP POST endpoint for user login
  [HttpPost("login")] // Endpoint: account/login
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
    // Retrieve the user from the database based on the username
    var user = await context.Users.FirstOrDefaultAsync(user => user.UserName == loginDto.Username.ToLower());

    // If the user does not exist, return an unauthorized response
    if (user == null) return Unauthorized("Invalid username");

    // Create a new HMACSHA512 instance using the user's stored salt
    using var hmac = new HMACSHA512(user.PasswordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password)); // Compute hash of the input password

    // Compare the computed hash with the stored hash byte by byte
    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password"); // Return unauthorized if mismatch
    }

    return new UserDto
    {
      Username = user.UserName,
      Token = tokenService.CreateToken(user),
    }; // Return the authenticated user
  }

  // Helper method to check if a username already exists in the database
  private async Task<bool> UserExists(string username)
  {
    // Check if any user exists with the given username (case-insensitive)
    return await context.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower());
  }
}
