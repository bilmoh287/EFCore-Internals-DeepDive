using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TrainingCenter.Data;
using TrainingCenter.Entities;


// Configuration setup
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

string? connectionString = configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("Connection string not found.");
    return;
}

// Create options with logging
var options =
    new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .Options;


using var context = new AppDbContext(options);

Console.WriteLine("Connected successfully.");
Console.WriteLine();


// Call examples
GetFilteredStudents(context);


static void GetFilteredStudents(AppDbContext context)
{
    Console.WriteLine("Filtered Projection With Sorting");
    Console.WriteLine("--------------------------------");
    Console.WriteLine();

    var query = context.Students
        .Where(s => s.Status == "Active")
        .Select(s => new { s.StudentId, Fullname = s.FirstName + ' ' + s.LastName })
        .OrderBy(s => s.Fullname)
        .ThenBy(s => s.StudentId);


    // Preview SQL before execution
    PreviewSQLUsingToQueryString(query.ToQueryString());

    // Execute query
    var students = query.ToList();

    // Print resultsa
    Console.WriteLine("\n\nStudent Names:");
    Console.WriteLine("--------------");

    foreach (var student in students)
    {
        Console.WriteLine($"{student.StudentId} {student.Fullname}");
    }

    Console.WriteLine();
    Console.WriteLine($"\nTotal Students: {students.Count}");
    Console.WriteLine();
}

static void PreviewSQLUsingToQueryString(string SQLString)
{
    Console.WriteLine("\nPreview SQL using ToQueryString():");
    Console.WriteLine("----------------------------------");
    Console.WriteLine(SQLString);
    Console.WriteLine();

}