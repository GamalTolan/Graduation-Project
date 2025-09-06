using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Models
{
    public enum Role
    {
        Admin,
        Instructor,
        Trainee
    }
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }=string.Empty;

        [Required,EmailAddress]
        public string Email { get; set; }

        public Role Role { get; set; } 

        public ICollection<Course>? Courses { get; set; } 
        public ICollection<Grade>? Grades { get; set; }   

    }
}
