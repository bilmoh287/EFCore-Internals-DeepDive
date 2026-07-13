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
ShowStudentEnrollmentsUsingSelectMany(context);
ShowStudentEnrollmentsUsingSelect(context);


/// <summary>
/// Shows student enrollments by flattening Students -> Enrollments using SelectMany().
/// </summary>
static void ShowStudentEnrollmentsUsingSelectMany(AppDbContext context)
{
    Console.WriteLine("Student Enrollments Using SelectMany()");
    Console.WriteLine("--------------------------------------");
    Console.WriteLine();

    // Build query first
    var query = context.Students
        .SelectMany(
            student => student.Enrollments,
            (student, enrollment) => new
            {
                student.StudentId,
                StudentName = student.FirstName + " " + student.LastName,
                enrollment.CourseId,
                enrollment.Status
            })
        .OrderBy(x => x.StudentId);

    // Preview SQL before execution
    PreviewSQLUsingToQueryString(query.ToQueryString());

    // Execute query
    var report = query.ToList();

    Console.WriteLine("Student Course Registrations:");
    Console.WriteLine("-----------------------------");
    Console.WriteLine();

    foreach (var row in report)
    {
        Console.WriteLine(
            $"{row.StudentId} - {row.StudentName} - Course: {row.CourseId} - {row.Status}");
    }

    Console.WriteLine();
    Console.WriteLine($"Total Registrations: {report.Count}");
}

static void ShowStudentEnrollmentsUsingSelect(AppDbContext context)
{
    Console.WriteLine("Students With Their Courses - Select()");
    Console.WriteLine("---------------------------------------");
    Console.WriteLine();

    // Build query first
    var query =
        context.Students
               .Select(s => new
               {
                   StudentId = s.StudentId,
                   StudentName = s.FirstName + " " + s.LastName,

                   Courses = s.Enrollments
                              .Select(e => e.Course.Title)
                              .ToList()
               })
               .OrderBy(s => s.StudentId);

    // Preview SQL before execution
    PreviewSQLUsingToQueryString(query.ToQueryString());

    // Execute query
    var students = query.ToList();

    Console.WriteLine("Student Report:");
    Console.WriteLine("---------------");
    Console.WriteLine();

    foreach (var student in students)
    {
        Console.WriteLine($"{student.StudentId} - {student.StudentName}");

        foreach (var course in student.Courses)
        {
            Console.WriteLine($"   • {course}");
        }

        Console.WriteLine();
    }

    Console.WriteLine($"Total Students: {students.Count}");
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