using Graduation_Project.BLL.Pagination;
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
                Description= c.Description,
                StartDate= c.StartDate,
                EndDate= c.EndDate,
                Category = c.Category,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor?.Name ?? "N/A"
            }).ToList();
        }

        public CourseDetailsVM? GetById(int id)
        {
            Course course = _unitOfWork.Courses.GetById(id);
            if (course == null) return null;

            return new CourseDetailsVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                InstructorId = (int)course.InstructorId,
                InstructorName = course.Instructor.Name ?? "N/A"
            };
        }

        public void Add(CreateCourseVM vm)
        {
            Course course = new Course
            {
                Name = vm.Name,
                Description = vm.Description,
                Category = vm.Category,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                InstructorId = vm.InstructorId

            };

            _unitOfWork.Courses.Add(course);
            _unitOfWork.Save();
        }
        public EditCourseVM? GetForEdit(int id)
        {
            Course? course = _unitOfWork.Courses.GetById(id);

            return new EditCourseVM()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Category = course.Category,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                InstructorId = (int) course.InstructorId
            };
        }

        public void Update(EditCourseVM editCourseVM)
        {
            Course course = _unitOfWork.Courses.GetById(editCourseVM.Id);
            if (course != null)
            {
                course.Name = editCourseVM.Name;
                course.Description = editCourseVM.Description;
                course.Category = editCourseVM.Category;
                course.StartDate = editCourseVM.StartDate;
                course.EndDate = editCourseVM.EndDate;
                course.InstructorId = editCourseVM.InstructorId;

                _unitOfWork.Courses.Update(course);
                _unitOfWork.Save();
            }
        }

        public void Delete(int id)
        {
            _unitOfWork.Courses.Delete(id);
            _unitOfWork.Save();
        }

        

        public bool IsNameExists(string name)
        {
            return _unitOfWork.Courses.IsCourseNameExists(name);
        }

        public PageResult<CourseVM> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var courseVM = _unitOfWork.Courses.GetAllWithPagination(pageNumber, pageSize).Select(c => new CourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Category = c.Category,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                InstructorName =c.Instructor.Name ?? "N/A",
            }).ToList();

            return new PageResult<CourseVM>()
            {
                Items = courseVM,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _unitOfWork.Courses.GetTotalCount()
            };
        }
    }
}
