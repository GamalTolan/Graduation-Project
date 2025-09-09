using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.UserVM;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /Users
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var users = _userService.GetAllWithPagination(pageNumber, pageSize);
            return View(users);
        }

        // GET: /Users/Details/5
        public IActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            return View(new CreateUserVM());
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (_userService.IsEmailExists(vm.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(vm);
            }

            _userService.Add(vm);
            TempData["Success"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Users/Edit/5
        public IActionResult Edit(int id)
        {
            var user = _userService.GetForEdit(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _userService.Update(vm);
            TempData["Success"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Users/Delete/5
        //public IActionResult Delete(int id)
        //{
        //    var user = _userService.GetById(id);
        //    if (user == null) return NotFound();

        //    return View(user);
        //}

        //// POST: /Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    _userService.Delete(id);
        //    TempData["Success"] = "User deleted successfully.";
        //    return RedirectToAction(nameof(Index));
        //}
        // GET: User/Delete/5
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Remote Validation: check if email is unique (AJAX)
        [AcceptVerbs("Get", "Post")]
        public IActionResult IsEmailUnique(string email)
        {
            if (_userService.IsEmailExists(email))
            {
                return Json($"Email '{email}' is already in use.");
            }

            return Json(true);
        }
    }
}