using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Models
{
    public enum Category
    {
        Programming,
        Design,
        Marketing
    }
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course Name must be 3–50 characters")]
        public string Name { get; set; }=string.Empty;
        [Required(ErrorMessage = "Category is required")]
        [StringLength(50)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Category Category { get; set; }
        public int? InstructorId { get; set; }
        public User? Instructor { get; set; }
        public ICollection<Session>? Sessions { get; set; }

    }
}
