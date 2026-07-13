/*
========================================================
Code Overview
--------------------------------------------------------
Purpose:
- Compare 3 approaches:
  1. BAD -> N+1 problem
  2. BETTER -> Include()
  3. BEST -> Projection
- Show SQL preview before execution
- Demonstrate performance-friendly query design

Key Points:
- N+1 = multiple queries
- Include() = single query but may load extra data
- Projection = single query with optimized selected data
- ToQueryString() previews SQL query shape
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


// Call main methods
MoreExamplesOnBestApproches(context);
static void PrintSeparator()
{
    Console.WriteLine(new string('-', 60));
    Console.WriteLine();
}

static void MoreExamplesOnBestApproches(AppDbContext context)
{
    var students =
    context.Students
           .Select(s => new
           {
               FullName = s.FirstName + " " + s.LastName,
               City = s.StudentProfile.City
           })
           .ToList();

    Console.WriteLine();
    Console.WriteLine("Example of Student and Profile");
    foreach (var student in students)
    {
        Console.WriteLine(
            $"{student.FullName}  - {student.City}");
    }
    Console.WriteLine();

    var TeachersTeaches = context.Instructors
        .Select(s => new
        {
            s.FirstName,
            TeachingCourses = s.Courses.Count()
        })
        .OrderBy(s => s.FirstName)
        .ToList();

    Console.WriteLine();
    Console.WriteLine("Example of Instructors Teaching Courses");
    Console.WriteLine();

    foreach (var Instructor in TeachersTeaches)
    {
        Console.WriteLine(
            $"{Instructor.FirstName} Teaches =  {Instructor.TeachingCourses}");
    }
    Console.WriteLine();

    var Courses = context.Courses
    .Select(c => new
    {
        c.Title,
        InstructorName = c.Instructor.FirstName
    })
    .ToList();

    Console.WriteLine();
    Console.WriteLine("Example of Courses List with Instructore");
    Console.WriteLine();

    foreach (var course in Courses)
    {
        Console.WriteLine(
            $"{course.Title} \nInstructor:  {course.InstructorName}");
        Console.WriteLine();
    }
    Console.WriteLine();

    var EnrollmentList = context.Enrollments
        .Select(E => new
        {
            Student = E.Student.FirstName,
            Course = E.Course.Title,
            E.EnrollmentDate,

        })
        .OrderBy(E => E.EnrollmentDate)
        .ToList();

    Console.WriteLine();
    Console.WriteLine("Example of Students Enrollment in Courses");
    Console.WriteLine();

    foreach (var E in EnrollmentList)
    {
        Console.WriteLine(
            $"{E.Student} Enrolled in: {E.Course} at: {E.EnrollmentDate}");
        Console.WriteLine();
    }
    Console.WriteLine();

    var query =
        context.Enrollments
               .GroupBy(e => new
               {
                   StudentName =
                       e.Student.FirstName + " " + e.Student.LastName,

                   CourseTitle =
                       e.Course.Title
               })
               .Select(g => new
               {
                   g.Key.StudentName,
                   g.Key.CourseTitle,
                   TimesEnrolled = g.Count()
               })
               .OrderBy(e => e.StudentName);

    PreviewSQLUsingToQueryString(query.ToQueryString());
    var result = query.ToList();

    Console.WriteLine();
    Console.WriteLine("Example of Students Enrollment Times in a course");
    Console.WriteLine();

    foreach (var item in result)
    {
        Console.WriteLine(
            $"{item.StudentName,-10} {item.CourseTitle,-15} {item.TimesEnrolled}");
    }
    Console.WriteLine();
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