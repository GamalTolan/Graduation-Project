using Graduation_Project.BLL.ViewModels.GradeVM;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface IGradeService
    {
        IEnumerable<GradeVM> GetAll();
        GradeDetailsVM? GetById(int id);

        void Add(BaseGradeVM vm);  
        void Update(GradeVM vm);
        void Delete(int id);

        GradeVM? GetForEdit(int id);
    }
}
