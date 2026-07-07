// EF Core namespace
using Microsoft.EntityFrameworkCore;


// Configuration namespaces
using Microsoft.Extensions.Configuration;


// Your DbContext namespace
using TrainingCenter.Data;
using TrainingCenter.Entities;

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
    ? "Connected successfully to TrainingCenterDB.)"
    : "Could not connect to TrainingCenterDB.");

Console.WriteLine("================================================");

RetrieveAndPrintStudents(context);
static void RetrieveAndPrintStudents(AppDbContext context)
{
    // Build the query first (no execution yet)
    var query = context.Students
        .OrderBy(s => s.StudentId);

    // Show generated SQL before execution
    PrintGeneratedSql("Students", query.ToQueryString());

    // Execute query
    var students = query.ToList();

    // If no data exists, stop here
    if (students.Count == 0)
    {
        Console.WriteLine("No students found in the database.");
        Console.WriteLine();
        return;
    }

    Console.WriteLine("Students List:");
    Console.WriteLine("--------------");

    foreach (var student in students)
    {
        Console.WriteLine(
            $"Id: {student.StudentId}, " +
            $"Name: {student.FirstName} {student.LastName}, " +
            $"Email: {student.Email}, " +
            $"Status: {student.Status}, " +
            $"Phone: {student.PhoneNumber ?? "N/A"}");
    }

    Console.WriteLine();
    Console.WriteLine($"Total Students: {students.Count}");
    Console.WriteLine(new string('=', 70));
    Console.WriteLine();
}

static void PrintGeneratedSql(string tableName, string sqlQuery)
{
    Console.WriteLine($"Generated SQL Query for {tableName}:");
    Console.WriteLine(new string('-', 40));
    Console.WriteLine(sqlQuery);
    Console.WriteLine();
}