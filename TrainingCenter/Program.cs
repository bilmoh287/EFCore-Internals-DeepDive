/*
========================================================
Code Overview
--------------------------------------------------------
Purpose:
- Demonstrate Include()
- Demonstrate ThenInclude()
- Load related data from TrainingCenterDB
- Preview SQL using ToQueryString()
- Enable runtime logging

Key Points:
- Include() loads first-level related data
- ThenInclude() loads deeper related data
- ToQueryString previews query shape
- Runtime logging shows actual executed SQL
========================================================
*/

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

// Read connection string
string? connectionString =
    configuration.GetConnectionString("DefaultConnection");

// Validate values
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

// Create context
using var context = new AppDbContext(options);

Console.WriteLine("Connected successfully.");
Console.WriteLine();

// Call main method
ShowStudentsWithEnrollmentsAndCourses(context);


/// <summary>
/// Loads students with their enrollments and related courses.
/// </summary>
static void ShowStudentsWithEnrollmentsAndCourses(AppDbContext context)
{
    Console.WriteLine("Course Report Using Join()");
    Console.WriteLine("--------------------------");
    Console.WriteLine();

    // Build query first
    var query = context.Courses
        .Join(
            context.Instructors,
            course => course.InstructorId,
            Instructor => Instructor.InstructorId,
            (course, Instructor) => new
            {
                course.Title,
                course.Code,
                InstructorName =
                           Instructor.FirstName + " " + Instructor.LastName
            })
        .OrderBy(x => x.Title);

    // Preview SQL before execution
    PreviewSQLUsingToQueryString(query.ToQueryString());

    // Execute query
    var report = query.ToList();

    // Print readable output
    Console.WriteLine("Courses With Instructors:");
    Console.WriteLine("-------------------------");

    Console.WriteLine();
    foreach (var row in report)
    {
        Console.WriteLine(
            $"{row.Code} - {row.Title} - {row.InstructorName}");
    }

    Console.WriteLine();
    Console.WriteLine($"Total Courses: {report.Count}");
}


/// <summary>
/// Displays generated SQL before execution.
/// </summary>
static void PreviewSQLUsingToQueryString(string SQLString)
{
    Console.WriteLine("\nPreview SQL using ToQueryString():");
    Console.WriteLine("----------------------------------");
    Console.WriteLine(SQLString);
    Console.WriteLine();
}