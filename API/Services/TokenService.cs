using System.IdentityModel.Tokens.Jwt; // For working with JWT tokens
using System.Security.Claims; // For creating and managing claims in the token
using System.Text; // For encoding strings
using API.Entities; // Custom namespace for application-specific entities
using API.Interfaces; // Custom namespace for interface declarations
using Microsoft.IdentityModel.Tokens; // For token-related security utilities

namespace API.Services;

// TokenService class implements ITokenService interface
public class TokenService(IConfiguration config) : ITokenService
{
  // Method to create a JWT token for the given user
  public string CreateToken(AppUser user)
  {
    // Retrieve the token key from the configuration settings
    var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenkey from appsettings");

    // Ensure the token key has sufficient length for security
    if (tokenKey.Length < 64) throw new Exception("Your token key must be at least 64 characters");

    // Create a security key using the token key
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

    // Define the claims to include in the token, such as the user's username
    var claims = new List<Claim>
    {
      new(ClaimTypes.NameIdentifier, user.UserName) // Add the user's username as a claim
    };

    // Define the signing credentials using the security key and hashing algorithm
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    // Describe the token's properties, including claims, expiration, and credentials
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims), // Attach the claims
      Expires = DateTime.UtcNow.AddDays(7), // Set the token's expiration date
      SigningCredentials = creds, // Attach the signing credentials
    };

    // Create a JWT token handler to generate the token
    var tokenHanler = new JwtSecurityTokenHandler();

    // Create the token based on the descriptor
    var token = tokenHanler.CreateToken(tokenDescriptor);

    // Write the token as a string and return it
    return tokenHanler.WriteToken(token);
  }
}

