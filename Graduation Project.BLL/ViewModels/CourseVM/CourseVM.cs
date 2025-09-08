using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.CourseVM
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public Category Category { get; set; } ;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }
    }
}
