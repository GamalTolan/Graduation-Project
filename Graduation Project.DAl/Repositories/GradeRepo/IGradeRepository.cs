using Graduation_Project.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.DAl.Repositories.GradeRepo
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        IEnumerable<Grade> GetGradesByTraineeId(int traineeId);
    }
}
