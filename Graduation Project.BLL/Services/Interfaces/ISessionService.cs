using Graduation_Project.DAl.Models;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<Session> GetAll();
        Session GetById(int id);
        void Create(Session session);
        void Update(Session session);
        void Delete(int id);
        IEnumerable<Session> SearchByCourseName(string courseName);

    }
}
