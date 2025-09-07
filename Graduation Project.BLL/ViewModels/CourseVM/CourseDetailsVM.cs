using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.CourseVM
{
    public class CourseDetailsVM : BaseCourseVM
    {
        public int Id { get; set; }
        public string InstructorName { get; set; } = string.Empty;
    }
}
