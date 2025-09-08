using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.ViewModels.CourseVM;
using Graduation_Project.BLL.ViewModels.SessionVM;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionVM> GetAll();
        SessionDetailsVM? GetById(int id);
        PageResult<SessionVM> GetAllWithPagination(int pageNumber, int pageSize);

        void Add(CreateSessionVM vm);
        void Update(EditSessionVM vm);
        void Delete(int id);

        EditSessionVM? GetForEdit(int id);
        IEnumerable<SessionVM> GetByCourseName(string courseName);
    }
}
