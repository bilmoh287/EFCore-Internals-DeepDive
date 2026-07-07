using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter.Entities
{
    public partial class Student
    {
        // ------------------------------------------------------
        // Primary Key
        // ------------------------------------------------------
        // Unique ID for each student.
        //
        // Example:
        // 1, 2, 3, 4...
        // ------------------------------------------------------
        public int StudentId { get; set; }

        // ------------------------------------------------------
        // First Name
        // ------------------------------------------------------
        // Student first name.
        //
        // Examples:
        // Ahmed
        // Sara
        // Mohammed
        // ------------------------------------------------------
        public string FirstName { get; set; } = null!;

        // ------------------------------------------------------
        // Last Name
        // ------------------------------------------------------
        // Student family / last name.
        //
        // Examples:
        // Ali
        // Smith
        // Hassan
        // ------------------------------------------------------
        public string LastName { get; set; } = null!;

        // ------------------------------------------------------
        // Email
        // ------------------------------------------------------
        // Student email address.
        //
        // Usually unique in database.
        //
        // Example:
        // ahmed@email.com
        // ------------------------------------------------------
        public string Email { get; set; } = null!;

        // ------------------------------------------------------
        // DateOfBirth
        // ------------------------------------------------------
        // Student birth date.
        //
        // DateOnly stores date without time.
        //
        // Example:
        // 2004-08-15
        // ------------------------------------------------------
        public DateOnly DateOfBirth { get; set; }

        // ------------------------------------------------------
        // RegisteredAt
        // ------------------------------------------------------
        // Date and time when student joined system.
        //
        // Example:
        // 2026-04-20 10:30 AM
        // ------------------------------------------------------
        public DateTime RegisteredAt { get; set; }

        // ------------------------------------------------------
        // Status
        // ------------------------------------------------------
        // Current student state.
        //
        // Examples:
        // Active
        // Suspended
        // Graduated
        // Inactive
        // ------------------------------------------------------
        public string Status { get; set; } = null!;

        // ------------------------------------------------------
        // PhoneNumber
        // ------------------------------------------------------
        // Optional phone number.
        //
        // Nullable (?) means:
        // Student may not provide phone number.
        //
        // Example:
        // +9627XXXXXXXX
        // ------------------------------------------------------
        public string? PhoneNumber { get; set; }

        // ======================================================
        // Navigation Properties
        // ------------------------------------------------------
        // These properties represent relationships.
        // ======================================================

        // ------------------------------------------------------
        // Enrollments Relationship
        // ------------------------------------------------------
        // One student can enroll in many courses.
        //
        // Example:
        // Student enrolled in:
        // C#, SQL, Git
        // ------------------------------------------------------
        public virtual ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();

        // ------------------------------------------------------
        // StudentProfile Relationship
        // ------------------------------------------------------
        // One student has one profile.
        //
        // This is a One-to-One relationship.
        //
        // Example:
        // student.StudentProfile.Address
        // ------------------------------------------------------
        public virtual StudentProfile? StudentProfile { get; set; }
    }
}
