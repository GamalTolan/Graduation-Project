using Graduation_Project.DAl.Data;
using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.GradeRepo
{
    public class GradeRepository(AppDbContext context) : GenericRepository<Grade>(context), IGradeRepository
    {
        public IEnumerable<Grade> GetGradesByTraineeId(int traineeId)
        {
            return context.Grades.Where(g => g.TraineeId == traineeId).ToList();
        }
    }
}
