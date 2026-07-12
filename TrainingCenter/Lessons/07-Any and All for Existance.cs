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

//string? connectionString = configuration.GetConnectionString("DefaultConnection");

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


//using var context = new AppDbContext(options);

//Console.WriteLine("Connected successfully.");
//Console.WriteLine();


//// Call examples
//CheckData(context);

//static void CheckData(AppDbContext context)
//{
//    Console.WriteLine("Any() and All() Example");
//    Console.WriteLine("-----------------------");
//    Console.WriteLine();

//    // --------------------------------------------------
//    // Any() Example
//    // --------------------------------------------------

//    // Build query first
//    var activeStudentsQuery = context.Students
//        .Where(s => s.Status == "Active");

//    // Preview SQL query shape
//    PreviewSQLUsingToQueryString(activeStudentsQuery.ToQueryString());

//    bool hasActiveStudents = activeStudentsQuery.Any();

//    Console.WriteLine();
//    Console.WriteLine($"Has Active Students: {hasActiveStudents}");
//    Console.WriteLine();

//    // --------------------------------------------------
//    // All() Example
//    // --------------------------------------------------

//    // Build query first
//    var coursesQuery =
//        context.Courses;

//    // Preview SQL query shape
//    PreviewSQLUsingToQueryString(coursesQuery.ToQueryString());

//    // Execute query
//    // ToQueryString previews query shape,
//    // runtime logging shows actual executed SQL for All().
//    bool allCoursesValid =
//        coursesQuery.All(c => c.Price > 0);

//    Console.WriteLine();
//    Console.WriteLine($"All Courses Price > 0: {allCoursesValid}");
//    Console.WriteLine();
//}

//static void PreviewSQLUsingToQueryString(string SQLString)
//{
//    Console.WriteLine("\nPreview SQL using ToQueryString():");
//    Console.WriteLine("----------------------------------");
//    Console.WriteLine(SQLString);
//    Console.WriteLine();

//}