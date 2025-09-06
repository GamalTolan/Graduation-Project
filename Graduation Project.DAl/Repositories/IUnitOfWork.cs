using Graduation_Project.DAl.Repositories.CourseRepo;
using Graduation_Project.DAl.Repositories.GradeRepo;
using Graduation_Project.DAl.Repositories.SessionRepo;
using Graduation_Project.DAl.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICourseRepository Courses { get; }
        ISessionRepository Sessions { get; }
        IGradeRepository Grades { get; }
        void Save();
        
    }
}
