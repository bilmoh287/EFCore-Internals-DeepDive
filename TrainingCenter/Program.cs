/*
========================================================
Code Overview
--------------------------------------------------------
Purpose:
- Demonstrate Min() and Max()
- Show lowest and highest values
- Use TrainingCenterDB real schema
- Show SQL preview before execution

Key Points:
- Min() returns smallest value
- Max() returns largest value
- Database performs calculations
- ToQueryString() previews query shape
- Runtime logging shows actual executed SQL
========================================================
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TrainingCenter.Data;


// Configuration setup
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();


// Read connection string
string? connectionString =
    configuration.GetConnectionString("DefaultConnection");


// Validate values
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("Connection string not found.");
    return;
}


// Create options
var options =
    new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .Options;


// Create context
using var context = new AppDbContext(options);


// Test connection if relevant
if (!context.Database.CanConnect())
{
    Console.WriteLine("Could not connect to TrainingCenterDB.");
    return;
}

Console.WriteLine("Connected successfully.");
Console.WriteLine();


ShowDistinctStudentStatuses(context);
ShowStudentsPerStatusReport(context);
/// <summary>
/// Displays generated SQL before execution.
/// </summary>
/// 
static void ShowDistinctStudentStatuses(AppDbContext context)
{
    var query = context.Students
                       .Select(s => s.Status)
                       .Distinct();

    // Preview SQL before execution
    PreviewSQLUsingToQueryString(query.ToQueryString());

    var distinctStatuses = query.ToList();
    foreach (var status in distinctStatuses)
    {
        Console.WriteLine($"Distinct Status: {status}");
    }
}
static void ShowStudentsPerStatusReport(AppDbContext context)
{
    var query = context.Students
        .GroupBy(s => s.Status)
        .Select(g => new
        {
            Status = g.Key,
            TotalStudents = g.Count()
        })
        .OrderByDescending(s => s.TotalStudents);

    // Preview SQL before execution
    PreviewSQLUsingToQueryString(query.ToQueryString());

    var report = query.ToList();
    foreach (var r in report)
    {
        Console.WriteLine($"Report Status: {r}");
    }
}
static void PreviewSQLUsingToQueryString(string SQLString)
{
    Console.WriteLine("\nPreview SQL using ToQueryString():");
    Console.WriteLine("----------------------------------");
    Console.WriteLine(SQLString);
    Console.WriteLine();
}