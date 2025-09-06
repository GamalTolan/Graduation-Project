using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.CourseRepo
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        IEnumerable<Course> SearchByName(string name);
        IEnumerable<Course> SearchByCategory(Category category);
        void AssignInstructor(int courseId, int instructorId);
    }
}
