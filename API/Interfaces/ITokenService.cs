using API.Entities; // Importing the namespace containing application-specific entities

namespace API.Interfaces;

// Interface definition for a Token Service
public interface ITokenService
{
  // Method signature for creating a token
  // This method takes an AppUser object and returns a string (the generated token)
  string CreateToken(AppUser user);
}
