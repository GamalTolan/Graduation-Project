using Graduation_Project.BLL.ViewModels.SessionVM;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionVM> GetAll();
        SessionDetailsVM? GetById(int id);

        void Add(CreateSessionVM vm);
        void Update(EditSessionVM vm);
        void Delete(int id);

        EditSessionVM? GetForEdit(int id);
    }
}
