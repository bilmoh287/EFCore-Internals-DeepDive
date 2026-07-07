using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter.Entities
{
    public partial class Instructor
    {
        // ------------------------------------------------------
        // Primary Key
        // ------------------------------------------------------
        // Unique ID for each instructor.
        //
        // Example:
        // 1, 2, 3, 4...
        // ------------------------------------------------------
        public int InstructorId { get; set; }

        // ------------------------------------------------------
        // First Name
        // ------------------------------------------------------
        // Instructor first name.
        //
        // Examples:
        // Mohammed
        // Ahmed
        // Sara
        // ------------------------------------------------------
        public string FirstName { get; set; } = null!;

        // ------------------------------------------------------
        // Last Name
        // ------------------------------------------------------
        // Instructor family / last name.
        //
        // Examples:
        // Ali
        // Hassan
        // Smith
        // ------------------------------------------------------
        public string LastName { get; set; } = null!;

        // ------------------------------------------------------
        // Email
        // ------------------------------------------------------
        // Official email address for the instructor.
        //
        // Usually unique in database.
        //
        // Example:
        // mohammed@email.com
        // ------------------------------------------------------
        public string Email { get; set; } = null!;

        // ------------------------------------------------------
        // HireDate
        // ------------------------------------------------------
        // Date when the instructor joined the company.
        //
        // DateOnly stores date without time.
        //
        // Example:
        // 2024-01-15
        // ------------------------------------------------------
        public DateOnly HireDate { get; set; }

        // ------------------------------------------------------
        // Salary
        // ------------------------------------------------------
        // Monthly or yearly salary depending on system design.
        //
        // Decimal is used for money values.
        //
        // Examples:
        // 850.00
        // 1500.00
        // ------------------------------------------------------
        public decimal Salary { get; set; }

        // ------------------------------------------------------
        // ManagerId
        // ------------------------------------------------------
        // Self-referencing foreign key.
        //
        // Points to another instructor who is the manager.
        //
        // Nullable (?) means:
        // Some instructors may have no manager.
        //
        // Example:
        // ManagerId = 2
        // ------------------------------------------------------
        public int? ManagerId { get; set; }

        // ------------------------------------------------------
        // IsActive
        // ------------------------------------------------------
        // Indicates whether instructor is currently active.
        //
        // true  = active
        // false = inactive
        // ------------------------------------------------------
        public bool IsActive { get; set; }

        // ======================================================
        // Navigation Properties
        // ------------------------------------------------------
        // These properties represent relationships.
        // ======================================================

        // ------------------------------------------------------
        // Courses Relationship
        // ------------------------------------------------------
        // One instructor can teach many courses.
        //
        // Example:
        // Instructor teaches:
        // C#, SQL, EF Core
        // ------------------------------------------------------
        public virtual ICollection<Course> Courses { get; set; }
            = new List<Course>();

        // ------------------------------------------------------
        // InverseManager Relationship
        // ------------------------------------------------------
        // Collection of instructors managed by this instructor.
        //
        // Example:
        // Manager supervises 3 instructors.
        // ------------------------------------------------------
        public virtual ICollection<Instructor> InverseManager { get; set; }
            = new List<Instructor>();

        // ------------------------------------------------------
        // Manager Relationship
        // ------------------------------------------------------
        // Points to this instructor's manager.
        //
        // Example:
        // instructor.Manager.FirstName
        // ------------------------------------------------------
        public virtual Instructor? Manager { get; set; }
    }
}
