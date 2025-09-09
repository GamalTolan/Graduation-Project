using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.SessionVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Graduation_Project.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ICourseService _courseService; // To populate Course dropdown

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

        //// GET: Session/GetAll (without pagination)
        //public IActionResult GetAll()
        //{
        //    var sessions = _sessionService.GetAll();
        //    return View(sessions);
        //}

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
                TempData["SuccessMessage"] = "Session created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", vm.CourseId);
            TempData["ErrorMessage"] = "Failed to create session!";
            return View(vm);
        }

        // ✅ GET: Session/Edit/5
        public IActionResult Edit(int id)
        {
            var session = _sessionService.GetForEdit(id);
            if (session == null) return NotFound();

            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", session.CourseId);
            return View(session);
        }

        // ✅ POST: Session/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSessionVM vm)
        {
            if (ModelState.IsValid)
            {
                _sessionService.Update(vm);
                TempData["SuccessMessage"] = "Session updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", vm.CourseId);
            TempData["ErrorMessage"] = "Failed to update session!";
            return View(vm);
        }

        // ✅ GET: Session/Delete/5
        public IActionResult Delete(int id)
        {
            var session = _sessionService.GetById(id);
            if (session == null) return NotFound();
            return View(session);
        }

        // ✅ POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _sessionService.Delete(id);
            TempData["SuccessMessage"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
