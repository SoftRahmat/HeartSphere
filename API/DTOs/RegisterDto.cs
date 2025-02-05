using System.ComponentModel.DataAnnotations; // For data annotations like [Required]

namespace API.DTOs;

// Data Transfer Object (DTO) for handling user registration requests
public class RegisterDto
{
  // Username field is required for user registration
  [Required]
  public string Username { get; set; } = string.Empty;

  // Password field is required for user registration
  [Required]
  [StringLength(8, MinimumLength = 4)]
  public string Password { get; set; } = string.Empty;
}

