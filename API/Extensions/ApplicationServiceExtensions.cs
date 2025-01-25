using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
  {
    // Adds controllers for handling HTTP requests and responses
    services.AddControllers();

    // Configure the database context to use SQLite
    services.AddDbContext<DataContext>(option =>
    {
      option.UseSqlite(config.GetConnectionString("DefaultConnection")); // Use connection string from configuration
    });

    // Add CORS (Cross-Origin Resource Sharing) support to the application
    services.AddCors();

    // Register the ITokenService interface with its implementation TokenService
    services.AddScoped<ITokenService, TokenService>();

    return services;
  }
}
