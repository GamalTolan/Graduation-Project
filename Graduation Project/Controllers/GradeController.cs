using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.GradeVM;
using Graduation_Project.DAl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Graduation_Project.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        public GradeController(
            IGradeService gradeService,
            IUserService userService,
            ISessionService sessionService)
        {
            _gradeService = gradeService;
            _userService = userService;
            _sessionService = sessionService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var result = _gradeService.GetAllWithPagination(pageNumber, pageSize);
            return View(result);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View(new BaseGradeVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BaseGradeVM vm)
        {
            if (ModelState.IsValid)
            {
                if (_gradeService.IsGradeExists(vm.TraineeId, vm.SessionId))
                {
                    ModelState.AddModelError("", "This trainee already has a grade for this session.");
                    LoadDropdowns();
                    return View(vm);
                }

                _gradeService.Add(vm);
                TempData["SuccessMessage"] = "Grade created successfully!";
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var grade = _gradeService.GetById(id);
            if (grade == null) return NotFound();

            LoadDropdowns();
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GradeDetailsVM vm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.Update(vm);
                TempData["SuccessMessage"] = "Grade updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            var grade = _gradeService.GetById(id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _gradeService.Delete(id);
            TempData["SuccessMessage"] = "Grade deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdowns()
        {
            ViewBag.Trainees = _userService.GetAll()
                .Where(u => u.Role == Role.Trainee.ToString())
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }).ToList();

            ViewBag.Sessions = _sessionService.GetAll()
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.CourseName}"
                }).ToList();
        }
        public IActionResult Details(int id)
        {
            var grade = _gradeService.GetById(id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        public IActionResult SearchByCourse(string courseName, int pageNumber = 1, int pageSize = 5)
        {
            var grades = _gradeService.GetGradesByCourseName(courseName);
            if (!grades.Any())
                TempData["Message"] = "No grades found for this course.";

            var result = new PageResult<GradeVM>
            {
                Items = grades,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = grades.Count()
            };

            return View("Index", result);
        }

        public IActionResult SearchByTrainee(string traineeName, int pageNumber = 1, int pageSize = 5)
        {
            var grades = _gradeService.GetGradesByTraineeName(traineeName);
            if (!grades.Any())
                TempData["Message"] = "No grades found for this trainee.";
            var result = new PageResult<GradeVM>
            {
                Items = grades,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = grades.Count()
            };
            return View("Index", result);
        }

       
    }
}
