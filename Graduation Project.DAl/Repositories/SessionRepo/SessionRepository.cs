using Graduation_Project.DAl.Data;
using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.SessionRepo
{
    public class SessionRepository(AppDbContext context) : GenericRepository<Session>(context), ISessionRepository
    {
        public IEnumerable<Session> GetSessionsByCourseName(string courseName)
        {
            return context.Sessions.Where(s => s.Course.Name == courseName).ToList();
        }
    }
}
