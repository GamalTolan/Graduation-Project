using Graduation_Project.DAl.Data;
using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.CourseRepo
{
    public class CourseRepository(AppDbContext context) : GenericRepository<Course>(context), ICourseRepository
    {
        public void AssignInstructor(int courseId, int instructorId)
        {
             var course = context.Courses.Find(courseId);
                if (course != null)
                {
                    course.InstructorId = instructorId;
                    context.Courses.Update(course);
                }
        }

        public IEnumerable<Course> SearchByCategory(Category category)
        {
          return  context.Courses.Where(c => c.Category == category).ToList();

        }

        public IEnumerable<Course> SearchByName(string name)
        {
            return context.Courses.Where(c => c.Name==name).ToList();

        }
        public bool IsCourseNameExists(string courseName)
        {
            return context.Courses.Any(c => c.Name == courseName);
        }
    }
}
