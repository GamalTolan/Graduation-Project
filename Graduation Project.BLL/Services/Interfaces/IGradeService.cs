using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.ViewModels.GradeVM;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface IGradeService
    {
        void Add(BaseGradeVM vm);
        
        void Update(GradeDetailsVM vm);

        void Delete(int id);

        PageResult<GradeVM> GetAllWithPagination(int pageNumber, int pageSize); 
        PageResult<GradeVM> GetGradesByTrainee(int traineeId, int pageNumber, int pageSize);
        GradeDetailsVM? GetById(int id);
        bool IsGradeExists(int traineeId, int sessionId);
        IEnumerable<GradeVM> GetGradesByCourseName(string courseName);
        IEnumerable<GradeVM> GetGradesByTraineeName(string studentName);

    }
}
