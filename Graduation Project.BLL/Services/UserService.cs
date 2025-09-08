using Graduation_Project.BLL.Pagination;
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
            User user = _unitOfWork.Users.GetById(id);
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
            User user = new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Role = vm.Role
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
        }
        public EditUserVM? GetForEdit(int id)
        {
            User user = _unitOfWork.Users.GetById(id);
            if (user == null) return null;

            return new EditUserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
        public void Update(EditUserVM vm)
        {
            User user = _unitOfWork.Users.GetById(vm.Id);
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

       

        public bool IsEmailExists(string email)
        {
            return _unitOfWork.Users.GetAll().Any(u => u.Email == email);
        }
        public PageResult<UserVM> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var users = _unitOfWork.Users.GetAllWithPagination(pageNumber, pageSize)
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role.ToString()
                }).ToList();

            return new PageResult<UserVM>
            {
                Items = users,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _unitOfWork.Users.GetTotalCount()
            };
        }

        public IEnumerable<UserVM> SearchByName(string name)
        {
            return _unitOfWork.Users.SearchByName(name)
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role.ToString()
                }).ToList();
        }

        public IEnumerable<UserVM> SearchByRole(Role role)
        {
            return _unitOfWork.Users.SearchByRole(role)
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role.ToString()
                }).ToList();
        }
    }
}
