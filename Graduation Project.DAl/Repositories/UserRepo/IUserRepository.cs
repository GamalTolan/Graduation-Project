using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.UserRepo
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> SearchByName(string name);
        IEnumerable<User> SearchByRole(Role role);
        string GetTraineeNameById(int id);
       
    }
}
