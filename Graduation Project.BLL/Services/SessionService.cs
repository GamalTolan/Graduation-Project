using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.SessionVM;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Graduation_Project.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SessionVM> GetAll()
        {
            var sessions = _unitOfWork.Sessions.GetAll();
            return sessions.Select(s => new SessionVM
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                // شيل CourseId لأنه مش موجود في SessionVM
                CourseName = s.Course?.Name ?? "N/A"
            }).ToList();
        }

        public SessionDetailsVM? GetById(int id)
        {
            var session = _unitOfWork.Sessions.GetById(id);
            if (session == null) return null;

            return new SessionDetailsVM
            {
                Id = session.Id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                CourseName = session.Course?.Name ?? "N/A"
            };
        }


        public void Add(CreateSessionVM vm)
        {
            var session = new Session
            {
                CourseId = vm.CourseId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate
            };

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Save();
        }

        public void Update(EditSessionVM vm)
        {
            var session = _unitOfWork.Sessions.GetById(vm.Id);
            if (session == null) return;

            session.CourseId = vm.CourseId;
            session.StartDate = vm.StartDate;
            session.EndDate = vm.EndDate;

            _unitOfWork.Sessions.Update(session);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Sessions.Delete(id);
            _unitOfWork.Save();
        }

        public EditSessionVM? GetForEdit(int id)
        {
            var session = _unitOfWork.Sessions.GetById(id);
            if (session == null) return null;

            return new EditSessionVM
            {
                Id = session.Id,
                CourseId = session.CourseId,
                StartDate = session.StartDate,
                EndDate = session.EndDate
            };
        }
    }
}
