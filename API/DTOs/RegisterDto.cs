using System.ComponentModel.DataAnnotations; // For data annotations like [Required]

namespace API.DTOs;

// Data Transfer Object (DTO) for handling user registration requests
public class RegisterDto
{
  // Username field is required for user registration
  [Required]
  public required string Username { get; set; }

  // Password field is required for user registration
  [Required]
  public required string Password { get; set; }
}

