using Graduation_Project.DAl.Data;
using Graduation_Project.DAl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.GradeRepo
{
    public class GradeRepository(AppDbContext context) : GenericRepository<Grade>(context), IGradeRepository
    {
        public string GetCourseNameBySessionId(int sessionId)
        {
            var session = context.Sessions
            .Include(s => s.Course)
            .FirstOrDefault(s => s.Id == sessionId);

            return session?.Course?.Name;
        }
        public IEnumerable<Grade> GetGradesByTraineeId(int traineeId)
        {
            return context.Grades.Where(g => g.TraineeId == traineeId).ToList();
        }

        public IEnumerable<Grade> GetGradesByCourseName(string courseName)
        {
            return context.Grades
         .Include(g => g.Session)
         .ThenInclude(s => s.Course)
         .Include(g => g.Trainee)
         .Where(g => g.Session.Course.Name== courseName)
         .ToList();
        }

        

        public IEnumerable<Grade> GetGradesByTraineeName(string studentName)
        {
                return context.Grades
            .Include(g => g.Session)
            .ThenInclude(s => s.Course)
            .Include(g => g.Trainee)
            .Where(g => g.Trainee.Name == studentName).ToList();

        }
    }
}
