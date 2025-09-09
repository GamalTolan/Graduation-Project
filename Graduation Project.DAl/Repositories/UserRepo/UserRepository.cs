using Graduation_Project.DAl.Data;
using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.UserRepo
{
    public class UserRepository (AppDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public string GetTraineeNameById(int id)
        {
            var user = context.Users.Find(id);
            if (user != null && user.Role == Role.Trainee)
            {
                return user.Name;
            }
            return null;
        }

        public IEnumerable<User> SearchByName(string name)
        {
            return context.Users.Where(u => u.Name==name).ToList();
        }
        public IEnumerable<User> SearchByRole(Role role)
        {
            return context.Users.Where(u => u.Role == role).ToList();
        }
    }
}
