using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtentions
{
  public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Add authentication using JWT Bearer scheme
      .AddJwtBearer(options => // Configure JWT Bearer options
      {
        // Retrieve the TokenKey from the application configuration
        var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found");

        // Define the token validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true, // Ensure the token's signature is valid
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)), // Use the symmetric security key for validation
          ValidateIssuer = false, // Disable validation of the token's issuer
          ValidateAudience = false, // Disable validation of the token's audience
        };
      });

    return services;
  }
}
