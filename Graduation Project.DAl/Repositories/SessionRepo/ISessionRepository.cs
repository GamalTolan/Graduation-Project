using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.SessionRepo
{
    public interface ISessionRepository :IGenericRepository<Session>
    {
        IEnumerable<Session> GetSessionsByCourseName(string courseName);
       
        
    }
}
