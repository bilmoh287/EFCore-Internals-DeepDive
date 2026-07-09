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
FindStudentByIdUSingFind(context);
FindStudentByIdUSingFirstOrDefault(context);


static void FindStudentByIdUSingFind(AppDbContext context)
{

    var Student = context.Students.Find(1);
    var Studen2 = context.Students.Find(1);


    // Does not upport ToQueryString
    //PreviewSQLUsingToQueryString(Student.ToQueryString());

    PrintStudent(Student);
    PrintStudent(Studen2);

}

/// <summary>
/// FirstOrDefault() returns the first matching row,
/// or null if no row exists.
/// </summary>
static void FindStudentByIdUSingFirstOrDefault(AppDbContext context)
{
    Console.WriteLine("\nExample 2 - FirstOrDefault()");
    Console.WriteLine("----------------------------");

    var query = context.Students
        .Where(s => s.StudentId == 1);

    // Preview query shape
    PreviewSQLUsingToQueryString(query.ToQueryString());

    // Execute query
    // Runtime logging will show the actual executed SQL.
    var student = query.FirstOrDefault();

    if (student == null)
    {
        Console.WriteLine("\nNo student found.");
    }
    else
    {
        Console.WriteLine("\nStudent Found:");
        Console.WriteLine($"{student.StudentId} - {student.FirstName} {student.LastName}");
    }
}

/// <summary>
/// Prints student information in readable format.
/// </summary>
static void PrintStudent(dynamic? student)
{
    if (student != null)
    {
        Console.WriteLine("\n\nStudent Found:");
        Console.WriteLine(
            $"{student.StudentId} - {student.FirstName} {student.LastName}");
    }
    else
    {
        Console.WriteLine("Student not found.");
    }

    Console.WriteLine();
}
static void PreviewSQLUsingToQueryString(string SQLString)
{
    Console.WriteLine("\nPreview SQL using ToQueryString():");
    Console.WriteLine("----------------------------------");
    Console.WriteLine(SQLString);
    Console.WriteLine();

}