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
  
        string GetCourseNameBySessionId(int sessionId);
        IEnumerable<Grade> GetGradesByTraineeId(int traineeId);
        IEnumerable<Grade> GetGradesByCourseName(string courseName);
        IEnumerable<Grade> GetGradesByTraineeName(string studentName);

    }
}
