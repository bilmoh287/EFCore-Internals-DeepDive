///*
//========================================================
//Code Overview
//--------------------------------------------------------
//Purpose:
//- Demonstrate AsNoTracking()
//- Show read-only query optimization
//- Use TrainingCenterDB real schema


//Key Points:
//- Better for lists and reports
//- No change tracking overhead
//- Faster for read-only screens
//========================================================
//*/


//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using TrainingCenter.Data;


//// Configuration
//IConfiguration configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false)
//    .Build();


//string? connectionString =
//    configuration.GetConnectionString("DefaultConnection");


//if (string.IsNullOrWhiteSpace(connectionString))
//{
//    Console.WriteLine("Connection string not found.");
//    return;
//}


//// DbContext
//var options = new DbContextOptionsBuilder<AppDbContext>()
//    .UseSqlServer(connectionString)
//    .Options;


//using var context = new AppDbContext(options);


//Console.WriteLine("Connected successfully.");
//Console.WriteLine();


//ShowStudents(context);


//static void ShowStudents(AppDbContext context)
//{
//    var students = context.Students
//        .AsNoTracking()
//        .Where(s => s.Status == "Active")
//        .OrderBy(s => s.StudentId)
//        .ToList();


//    foreach (var student in students)
//    {
//        Console.WriteLine(
//            $"{student.StudentId} - {student.FirstName} {student.LastName}");
//    }
//}
