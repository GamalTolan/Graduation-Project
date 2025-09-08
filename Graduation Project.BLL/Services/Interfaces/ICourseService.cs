using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.ViewModels.CourseVM;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface ICourseService
    {
        
        IEnumerable<CourseVM> GetAll();

        
        CourseDetailsVM? GetById(int id);

        PageResult<CourseVM> GetAllWithPagination(int pageNumber, int pageSize);
        void Add(CreateCourseVM vm);

        
        void Update(EditCourseVM vm);

        
        void Delete(int id);

        
        EditCourseVM? GetForEdit(int id);

        
        bool IsNameExists(string name);
    }
}
