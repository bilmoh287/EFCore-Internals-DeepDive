//// EF Core namespace
//using Microsoft.EntityFrameworkCore;


//// Configuration namespaces
//using Microsoft.Extensions.Configuration;


//// Your DbContext namespace
//using TrainingCenter.Data;
//using TrainingCenter.Entities;

//using Microsoft.Extensions.Logging;

//// Build configuration object so the console app can read appsettings.json
//IConfiguration configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory()) // Look for files in current folder
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
//    .Build();


//// Read the connection string from the ConnectionStrings section
//string? connectionString = configuration.GetConnectionString("DefaultConnection");

//// Check that connection string was found successfully
//if (string.IsNullOrWhiteSpace(connectionString))
//{
//    Console.WriteLine("Connection string 'DefaultConnection' was not found.");
//    return;
//}

///// Build DbContextOptions manually for AppDbContext
//var options = new DbContextOptionsBuilder<AppDbContext>()
//    .UseSqlServer(connectionString) // Tell EF Core to use SQL Server with this connection string
//    .LogTo(Console.WriteLine, LogLevel.Information)
//    .EnableSensitiveDataLogging()
//    .Options;

//// Create DbContext instance manually using the configured options
//using var context = new AppDbContext(options);

//// Test
//Console.WriteLine("================================================");

//Console.WriteLine(context.Database.CanConnect()
//    ? "Connected successfully to TrainingCenterDB.)"
//    : "Could not connect to TrainingCenterDB.");

//Console.WriteLine("================================================");

//// Run examples
//RetrieveAndPrintStudents(context);
//GetActiveStudentsCount(context);




///// <summary>
///// Example 1:
///// Retrieve active students
///// </summary>
//static void RetrieveAndPrintStudents(
//    AppDbContext context)
//{
//    Console.WriteLine("Example 1 - Retrieve Students");
//    Console.WriteLine("=============================");
//    Console.WriteLine();


//    // Build query first
//    var query = context.Students
//        .Where(s => s.Status == "Active")
//        .OrderBy(s => s.StudentId);


//    // Preview SQL
//    Console.WriteLine("Preview SQL using ToQueryString():");
//    Console.WriteLine("----------------------------------");
//    Console.WriteLine(query.ToQueryString());
//    Console.WriteLine();


//    // Execute query
//    var students = query.ToList();


//    Console.WriteLine(
//        $"Rows Returned: {students.Count}");


//    Console.WriteLine();
//    Console.WriteLine(new string('=', 70));
//    Console.WriteLine();
//}




///// <summary>
///// Example 2:
///// Show difference between ToQueryString() and Count() runtime SQL
///// </summary>
//static void GetActiveStudentsCount(
//    AppDbContext context)
//{
//    Console.WriteLine("Example 2 - Count Comparison");
//    Console.WriteLine("============================");
//    Console.WriteLine();


//    // Build query first
//    var query = context.Students
//        .Where(s => s.Status == "Active");


//    // Preview SQL before Count()
//    Console.WriteLine("Preview SQL using ToQueryString():");
//    Console.WriteLine("----------------------------------");
//    Console.WriteLine(query.ToQueryString());
//    Console.WriteLine();


//    Console.WriteLine(
//        "Now executing Count()...");
//    Console.WriteLine(
//        "Watch logging output above / below.");
//    Console.WriteLine();


//    // Actual execution
//    int total = query.Count();


//    Console.WriteLine(
//        $"Total Active Students: {total}");


//    Console.WriteLine();
//    Console.WriteLine(
//        "Important Note:");
//    Console.WriteLine(
//        "ToQueryString() previewed SELECT rows query.");
//    Console.WriteLine(
//        "But logging shows final executed COUNT(*) query.");


//    Console.WriteLine();
//    Console.WriteLine(new string('=', 70));
//    Console.WriteLine();
//}