using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories.CourseRepo;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<Course> GetAll() => _courseRepository.GetAll();

        public Course GetById(int id) => _courseRepository.GetById(id);

        public void Create(Course course) => _courseRepository.Add(course);

        public void Update(Course course) => _courseRepository.Update(course);

        public void Delete(int id) => _courseRepository.Delete(id);

        public IEnumerable<Course> SearchByName(string name) => _courseRepository.SearchByName(name);

        public IEnumerable<Course> SearchByCategory(Category category) => _courseRepository.SearchByCategory(category);

        public void AssignInstructor(int courseId, int instructorId) => _courseRepository.AssignInstructor(courseId, instructorId);
    }
}
