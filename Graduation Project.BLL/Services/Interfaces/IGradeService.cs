using Graduation_Project.DAl.Models;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetAll();
        Grade GetById(int id);
        void Create(Grade grade);
        void Update(Grade grade);
        void Delete(int id);
        IEnumerable<Grade> GetGradesByTraineeId(int traineeId);
    }
}
