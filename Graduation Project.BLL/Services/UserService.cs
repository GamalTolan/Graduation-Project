using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.UserVM;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Graduation_Project.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserVM> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            return users.Select(u => new UserVM
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString()   
            }).ToList();
        }

        public UserVM? GetById(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null) return null;

            return new UserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()   
            };
        }


        public void Add(CreateUserVM vm)
        {
            var user = new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Role = vm.Role
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
        }

        public void Update(EditUserVM vm)
        {
            var user = _unitOfWork.Users.GetById(vm.Id);
            if (user == null) return;

            user.Name = vm.Name;
            user.Email = vm.Email;
            user.Role = vm.Role;

            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();
        }

        public EditUserVM? GetForEdit(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null) return null;

            return new EditUserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public bool IsEmailExists(string email)
        {
            return _unitOfWork.Users.GetAll().Any(u => u.Email == email);
        }
    }
}
