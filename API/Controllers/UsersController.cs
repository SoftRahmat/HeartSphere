using API.Data; // Namespace for application-specific data context
using API.Entities; // Namespace for application-specific entities
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // For building API controllers
using Microsoft.EntityFrameworkCore; // For Entity Framework Core functionalities

namespace API.Controllers;

// UsersController inherits from BaseApiController
// Handles user-related API endpoints
public class UsersController(DataContext context) : BaseApiController
{
  [AllowAnonymous]
  // HTTP GET endpoint to retrieve all users
  [HttpGet] // Endpoint: /api/users
  public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
  {
    // Retrieve all users from the database and convert to a list
    var users = await context.Users.ToListAsync();

    // Return the list of users as the response
    return users;
  }

  [Authorize]
  // HTTP GET endpoint to retrieve a single user by ID
  [HttpGet("{id:int}")] // Endpoint: /api/users/{id}, where {id} must be an integer
  public async Task<ActionResult<AppUser>> GetUser(int id)
  {
    // Find the user in the database by ID
    var user = await context.Users.FindAsync(id);

    // If no user is found, return a 404 Not Found response
    if (user == null) return NotFound();

    // Return the found user as the response
    return user;
  }
}
