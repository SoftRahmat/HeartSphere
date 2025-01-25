namespace API.DTOs;

// Data Transfer Object (DTO) for handling login requests
public class LoginDto
{
  // The username provided by the user during login
  public required string Username { get; set; }

  // The password provided by the user during login
  public required string Password { get; set; }
}
