// EF Core namespace
using Microsoft.EntityFrameworkCore;


// Configuration namespaces
using Microsoft.Extensions.Configuration;


// Your DbContext namespace
using TrainingCenter.Data;

// Build configuration object so the console app can read appsettings.json
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Look for files in current folder
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
    .Build();


// Read the connection string from the ConnectionStrings section
string? connectionString = configuration.GetConnectionString("DefaultConnection");

// Check that connection string was found successfully
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("Connection string 'DefaultConnection' was not found.");
    return;
}

/// Build DbContextOptions manually for AppDbContext
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(connectionString) // Tell EF Core to use SQL Server with this connection string
    .Options;

// Create DbContext instance manually using the configured options
using var context = new AppDbContext(options);

// Test
Console.WriteLine("================================================");

Console.WriteLine(context.Database.CanConnect()
    ? "Connected! You are ready to retrieve Data :-)"
    : "Failed to connect.");

Console.WriteLine("================================================");
