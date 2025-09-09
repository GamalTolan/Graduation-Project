using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.Services;
using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.CourseVM;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Graduation_Project.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public CourseController(ICourseService courseService,IUserService userService)
        {
            _courseService = courseService;
            _userService =  userService;
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
            TempData["Success"] = "Course created successfully.";
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
            TempData["Success"] = "Course updated successfully.";
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
            TempData["Success"] = "Course deleted successfully.";
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
    }
}

