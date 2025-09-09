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
    }
}
