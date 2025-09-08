using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories.UserRepo;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll() => _userRepository.GetAll();

        public User GetById(int id) => _userRepository.GetById(id);

        public void Create(User user) => _userRepository.Add(user);

        public void Update(User user) => _userRepository.Update(user);

        public void Delete(int id) => _userRepository.Delete(id);

        public IEnumerable<User> SearchByName(string name) =>
            _userRepository.SearchByName(name);

        public IEnumerable<User> SearchByRole(string role)
        {
            if (Enum.TryParse<Role>(role, true, out var parsedRole))
                return _userRepository.SearchByRole(parsedRole);

            return new List<User>();
        }

    }
}
