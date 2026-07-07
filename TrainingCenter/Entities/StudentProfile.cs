using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCenter.Entities
{
    public partial class StudentProfile
    {
        public int StudentId { get ; set; }
        public string? Address { get ; set; }
        public string? City { get ; set; }
        public string? Country { get ; set; }
        public string? Bio { get ; set; }
        public string? LinkedInUrl { get ; set; }

        public virtual Student Student { get; set; } = null!;
    }
}
