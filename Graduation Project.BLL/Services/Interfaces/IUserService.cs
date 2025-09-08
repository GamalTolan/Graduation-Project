using Graduation_Project.DAl.Models;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
        IEnumerable<User> SearchByName(string name);
        IEnumerable<User> SearchByRole(string role);
    }
}
