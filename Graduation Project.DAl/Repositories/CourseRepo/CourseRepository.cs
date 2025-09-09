using Graduation_Project.DAl.Data;
using Graduation_Project.DAl.Models;
using Microsoft.EntityFrameworkCore;
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
          return  context.Courses.Include(c => c.Instructor).Where(c => c.Category == category).ToList();

        }

        public IEnumerable<Course> SearchByName(string name)
        {
            return context.Courses.Include(c => c.Instructor).Where(c => c.Name==name).ToList();

        }
        public bool IsCourseNameExists(string courseName)
        {
            return context.Courses.Any(c => c.Name == courseName);
        }
        public Course GetByIdWithInstructor(int id)
        {
            return context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.Id == id);
        }
        public IEnumerable<Course> GetAllWithInstructor()
        {
            return context.Courses
                .Include(c => c.Instructor)
                .ToList();
        }

        public string GetCourseName(int courseId)
        {
            var course = context.Courses.FirstOrDefault(C => C.Id== courseId);
            return course != null ? course.Name : null;
        }
    }
}
