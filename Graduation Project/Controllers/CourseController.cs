using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.Services;
using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.CourseVM;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Graduation_Project.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        

        public CourseController(ICourseService courseService,IUserService userService ,IUnitOfWork unitOfWork)
        {
            _courseService = courseService;
            _userService =  userService;
            _unitOfWork = unitOfWork;
        }

        // GET: /Courses
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            PageResult<CourseVM> courses = _courseService.GetAllWithPagination(pageNumber, pageSize);
            return View(courses);
        }

        // GET: /Courses/Details/5
        public IActionResult Details(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        public IActionResult Create()
        {
            ViewBag.Instructors = new SelectList(_userService.GetInstructors(), "Id", "Name");
            return View(new CreateCourseVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCourseVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = new SelectList(_userService.GetInstructors(), "Id", "Name", vm.InstructorId);
                return View(vm);
            }

            if (_courseService.IsNameExists(vm.Name))
            {
                ModelState.AddModelError("Name", "Course name already exists.");
                ViewBag.Instructors = new SelectList(_userService.GetInstructors(), "Id", "Name", vm.InstructorId);
                return View(vm);
            }

            _courseService.Add(vm);
            TempData["Success2"] = "Course created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Courses/Edit/5
        
        public IActionResult Edit(int id)
        {
            var course = _courseService.GetForEdit(id);
            if (course == null) return NotFound();

            ViewBag.Instructors = new SelectList(
                _userService.GetInstructors(), "Id", "Name", course.InstructorId
            );

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCourseVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = new SelectList(_userService.GetInstructors(), "Id", "Name", vm.InstructorId);
                return View(vm);
            }

            _courseService.Update(vm);
            TempData["Success2"] = "Course updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.Delete(id);
            TempData["Success2"] = "Course deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CheckCourseNameIsUnique(string Name)
        {
            var course = _courseService.IsNameExists(Name);
            if (course)
            {
                return Json($"Course name {Name} is already in use.");
            }
            return Json(true);

        }

        public IActionResult Search(string name, Category? category)
        {
            IEnumerable<CourseVM> results;

            if (!string.IsNullOrEmpty(name))
            {
                results = _courseService.SearchByName(name);
            }
            else if (category.HasValue)
            {
                results = _courseService.SearchByCategory(category.Value);
            }
            else
            {
                results = new List<CourseVM>();
            }

            return View("Index", new PageResult<CourseVM>
            {
                Items = results,
                PageNumber = 1,
                PageSize = results.Count(),
                TotalItems = results.Count()
            });
        }

    }
}

