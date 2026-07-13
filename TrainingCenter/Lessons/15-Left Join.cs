///*
//========================================================
//Code Overview
//--------------------------------------------------------
//Purpose:
//- Demonstrate Include()
//- Demonstrate ThenInclude()
//- Load related data from TrainingCenterDB
//- Preview SQL using ToQueryString()
//- Enable runtime logging

//Key Points:
//- Include() loads first-level related data
//- ThenInclude() loads deeper related data
//- ToQueryString previews query shape
//- Runtime logging shows actual executed SQL
//========================================================
//*/

//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using TrainingCenter.Data;
//using TrainingCenter.Entities;

//// Configuration setup
//IConfiguration configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false)
//    .Build();

//// Read connection string
//string? connectionString =
//    configuration.GetConnectionString("DefaultConnection");

//// Validate values
//if (string.IsNullOrWhiteSpace(connectionString))
//{
//    Console.WriteLine("Connection string not found.");
//    return;
//}

//// Create options with logging
//var options =
//    new DbContextOptionsBuilder<AppDbContext>()
//        .UseSqlServer(connectionString)
//        .LogTo(Console.WriteLine, LogLevel.Information)
//        .EnableSensitiveDataLogging()
//        .Options;

//// Create context
//using var context = new AppDbContext(options);

//Console.WriteLine("Connected successfully.");
//Console.WriteLine();

//// Call main method
//ShowStudentsWithAndWithoutProfile(context);


///// <summary>
///// Loads students with their enrollments and related courses.
///// </summary>
//static void ShowStudentsWithAndWithoutProfile(AppDbContext context)
//{
//    Console.WriteLine("Students With Profiles - Left Join");
//    Console.WriteLine("----------------------------------");
//    Console.WriteLine();

//    // Build query first
//    var report =
//        from s in context.Students
//        join p in context.StudentProfiles
//            on s.StudentId equals p.StudentId
//            into ProfileFroup
//        from p in ProfileFroup.DefaultIfEmpty()
//        select new
//        {
//            s.StudentId,
//            StudentName = s.FirstName + " " + s.LastName,
//            Country = p.Country != null ? p.Country : "No Profile",
//            City = p.City != null ? p.City : "No Profile"
//        };

//    // Apply sorting
//    var query =
//        report.OrderBy(x => x.StudentId);

//    // Preview SQL before execution
//    PreviewSQLUsingToQueryString(query.ToQueryString());

//    // Execute query
//    var result = query.ToList();

//    // Print readable output
//    Console.WriteLine("Student Report:");
//    Console.WriteLine("---------------");

//    Console.WriteLine();
//    foreach (var row in result)
//    {
//        Console.WriteLine(
//            $"{row.StudentId} - {row.StudentName} - {row.City} - {row.Country}");
//    }

//    Console.WriteLine();
//    Console.WriteLine($"Total Students: {result.Count}");
//}

///// <summary>
///// Displays generated SQL before execution.
///// </summary>
//static void PreviewSQLUsingToQueryString(string SQLString)
//{
//    Console.WriteLine("\nPreview SQL using ToQueryString():");
//    Console.WriteLine("----------------------------------");
//    Console.WriteLine(SQLString);
//    Console.WriteLine();
//}