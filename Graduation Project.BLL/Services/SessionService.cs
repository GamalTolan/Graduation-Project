using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories.SessionRepo;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public IEnumerable<Session> GetAll() => _sessionRepository.GetAll();

        public Session GetById(int id) => _sessionRepository.GetById(id);

        public void Create(Session session) => _sessionRepository.Add(session);

        public void Update(Session session) => _sessionRepository.Update(session);

        public void Delete(int id) => _sessionRepository.Delete(id);

        public IEnumerable<Session> SearchByCourseName(string courseName) =>
    _sessionRepository.GetSessionsByCourseName(courseName);

    }
}
