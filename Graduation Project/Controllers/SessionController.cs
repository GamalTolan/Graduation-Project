using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.SessionVM;
using Graduation_Project.DAl.Repositories.CourseRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Graduation_Project.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ICourseService _courseService; 
        


        public SessionController(ISessionService sessionService, ICourseService courseService)
        {
            _sessionService = sessionService;
            _courseService = courseService;
        }

        // GET: Session
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var result = _sessionService.GetAllWithPagination(pageNumber, pageSize);
            return View(result);
        }

       

        // GET: Session/Details/5
        public IActionResult Details(int id)
        {
            var session = _sessionService.GetById(id);
            if (session == null) return NotFound();
            return View(session);
        }

        // GET: Session/Create
        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name");
            return View(new CreateSessionVM());
        }

        // POST: Session/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSessionVM vm)
        {
            if (ModelState.IsValid)
            {
                _sessionService.Add(vm);
                TempData["SuccessMessage2"] = "Session created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", vm.CourseId);
            TempData["ErrorMessage"] = "Failed to create session!";
            return View(vm);
        }

        // GET: Session/Edit
        public IActionResult Edit(int id)
        {
            var session = _sessionService.GetForEdit(id);
            if (session == null) return NotFound();

            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", session.CourseId);
            return View(session);
        }

        // POST: Session/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSessionVM vm)
        {
            if (ModelState.IsValid)
            {
                _sessionService.Update(vm);
                TempData["SuccessMessage2"] = "Session updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", vm.CourseId);
            TempData["ErrorMessage"] = "Failed to update session!";
            return View(vm);
        }

        //GET: Session/Delete
        public IActionResult Delete(int id)
        {
            var session = _sessionService.GetById(id);
            if (session == null) return NotFound();
            return View(session);
        }

        // POST: Session/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _sessionService.Delete(id);
            TempData["SuccessMessage2"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string courseName, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.IsSearch = true;
            var sessions = _sessionService.GetByCourseName(courseName);
            if (!sessions.Any())
            {
                TempData["ErrorMessage"] = "No sessions found for this course name.";
            }
            var result = new PageResult<SessionVM>
            {
                Items = _sessionService.GetByCourseName(courseName).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _sessionService.GetByCourseName(courseName).Count()
            };

            return View("Index", result);
        }


    }
}
