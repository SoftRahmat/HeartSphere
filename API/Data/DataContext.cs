using API.Entities; // Namespace for application-specific entities
using Microsoft.EntityFrameworkCore; // For Entity Framework Core functionalities

namespace API.Data;

// DataContext class represents the database context
// It inherits from DbContext and is used to interact with the database
public class DataContext(DbContextOptions options) : DbContext(options)
{
  // DbSet property represents a collection of AppUser entities in the database
  // This corresponds to the "Users" table in the database
  public DbSet<AppUser> Users { get; set; }
}
