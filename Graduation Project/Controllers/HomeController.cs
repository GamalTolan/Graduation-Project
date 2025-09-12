using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels;
using Graduation_Project.BLL.ViewModels.DashboardVM;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly ISessionService _sessionService;
        private readonly IGradeService _gradeService;

        public HomeController(
            IUserService userService,
            ICourseService courseService,
            ISessionService sessionService,
            IGradeService gradeService)
        {
            _userService = userService;
            _courseService = courseService;
            _sessionService = sessionService;
            _gradeService = gradeService;
        }

        public IActionResult Index()
        {
            var vm = new DashboardVM
            {
                TotalUsers = _userService.GetAll().Count(),
                TotalCourses = _courseService.GetAll().Count(),
                TotalSessions = _sessionService.GetAll().Count(),
                TotalGrades = _gradeService.GetAll().Count()
            };

            return View(vm);
        }
    }
}
