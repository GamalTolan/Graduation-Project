using Graduation_Project.DAl.Data;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public ICourseRepository Courses { get; }
        public ISessionRepository Sessions { get; }
        public IGradeRepository Grades { get; }

        public UnitOfWork( AppDbContext context, IUserRepository userRepository, ICourseRepository courseRepository,ISessionRepository sessionRepository, IGradeRepository gradeRepository)
        {
            _context = context;
            Users = userRepository;
            Courses = courseRepository;
            Sessions = sessionRepository;
            Grades = gradeRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
