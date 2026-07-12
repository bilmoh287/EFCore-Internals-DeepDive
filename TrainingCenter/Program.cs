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
AggregateFuncion(context);

static void AggregateFuncion(AppDbContext context)
{
    Console.WriteLine("AVG(), SUM() and COUNT() Example");
    Console.WriteLine("-----------------------");
    Console.WriteLine();

    // --------------------------------------------------
    // COUNT() Example
    // --------------------------------------------------

    // Build query first
    var activeStudentsQuery = context.Students
        .Where(s => s.Status == "Active");

    // Preview SQL query shape
    PreviewSQLUsingToQueryString(activeStudentsQuery.ToQueryString());

    int total = activeStudentsQuery.Count();

    Console.WriteLine();
    Console.WriteLine($"Total Active Students: {total}");
    Console.WriteLine();

    // --------------------------------------------------
    // AVG() Example
    // --------------------------------------------------

    // Build query first
    var coursesQuery =
        context.Courses;

    // Preview SQL query shape
    PreviewSQLUsingToQueryString(coursesQuery.ToQueryString());

    // Execute query
    // ToQueryString previews query shape,
    // runtime logging shows actual executed SQL for AVG().
    decimal AveragePrice = coursesQuery.Average(c => c.Price);

    Console.WriteLine();
    Console.WriteLine($"Average Courses Price : {AveragePrice}");
    Console.WriteLine();

    // --------------------------------------------------
    // SUM() Example
    // --------------------------------------------------

    // Build query first
    var coursesDuration =
        context.Courses
        .Sum(c => c.DurationHours);

    // Preview SQL query shape
    PreviewSQLUsingToQueryString(coursesQuery.ToQueryString());

    // Execute query
    // ToQueryString previews query shape,
    // runtime logging shows actual executed SQL for SUM().

    Console.WriteLine();
    Console.WriteLine($"SUN of Courses DurationHours : {coursesDuration}");
    Console.WriteLine();
}

static void PreviewSQLUsingToQueryString(string SQLString)
{
    Console.WriteLine("\nPreview SQL using ToQueryString():");
    Console.WriteLine("----------------------------------");
    Console.WriteLine(SQLString);
    Console.WriteLine();

}