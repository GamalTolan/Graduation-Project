using Graduation_Project.BLL.ViewModels;
using Graduation_Project.DAl.Models;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();
        Course GetById(int id);
        void Create(Course course);
        void Update(Course course);
        void Delete(int id);
        IEnumerable<Course> SearchByName(string name);
        IEnumerable<Course> SearchByCategory(Category category);
        void AssignInstructor(int courseId, int instructorId);
    }
}
