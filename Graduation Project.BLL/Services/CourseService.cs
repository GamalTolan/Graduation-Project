using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.CourseVM;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Graduation_Project.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CourseVM> GetAll()
        {
            var courses = _unitOfWork.Courses.GetAll();

            return courses.Select(c => new CourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Category = c.Category.ToString(),
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor?.Name ?? "N/A"
            }).ToList();
        }

        public CourseDetailsVM? GetById(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            if (course == null) return null;

            return new CourseDetailsVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                StartDate = default,
                EndDate = default,
                IsActive = false,
                InstructorId = (int)course.InstructorId,
                InstructorName = course.Instructor?.Name ?? "N/A"
            };
        }

        public void Add(CreateCourseVM vm)
        {
            var course = new Course
            {
                Name = vm.Name,
                Category = vm.Category,
                InstructorId = vm.InstructorId
              
            };

            _unitOfWork.Courses.Add(course);
            _unitOfWork.Save();
        }

        public void Update(EditCourseVM vm)
        {
            var course = _unitOfWork.Courses.GetById(vm.Id);
            if (course == null) return;

            course.Name = vm.Name;
            course.Category = vm.Category;
            course.InstructorId = vm.InstructorId;
          

            _unitOfWork.Courses.Update(course);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Courses.Delete(id);
            _unitOfWork.Save();
        }

        public EditCourseVM? GetForEdit(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            if (course == null) return null;

            return new EditCourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                InstructorId = (int)course.InstructorId
            };
        }

        public bool IsNameExists(string name)
        {
            return _unitOfWork.Courses.GetAll().Any(c => c.Name == name);
        }
    }
}
