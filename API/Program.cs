using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args); // Create a builder for configuring the application

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build(); // Build the application

// Configure the HTTP request pipeline.
// Enable CORS for specific origins and allow any header/method
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader()
  .AllowAnyMethod()
  .WithOrigins("http://localhost:4300", "https://localhost:4300"));

// Add authentication middleware to the request pipeline
// Ensures that the application processes authentication for incoming requests
app.UseAuthentication();

// Add authorization middleware to the request pipeline
// Ensures that the application processes authorization for incoming requests, 
// verifying user permissions based on roles or policies
app.UseAuthorization();

// Map controller endpoints to the request pipeline
app.MapControllers();

// Run the application
app.Run();
