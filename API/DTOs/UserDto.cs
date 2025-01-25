namespace API.DTOs;

// Data Transfer Object (DTO) for returning user details after authentication
public class UserDto
{
  // The username of the authenticated user
  public required string Username { get; set; }

  // The authentication token issued to the user
  public required string Token { get; set; }
}
