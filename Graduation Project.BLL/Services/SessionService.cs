using Graduation_Project.BLL.Pagination;
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
       // private readonly ICourseService _courseService;


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
                CourseName = _unitOfWork.Courses.GetCourseName(s.CourseId),
                StartDate = s.StartDate,
                EndDate = s.EndDate
            }).ToList();
        }

        public SessionDetailsVM? GetById(int id)
        {
            Session session = _unitOfWork.Sessions.GetById(id);
            if (session == null) return null;

            return new SessionDetailsVM
            {
                Id = session.Id,
                CourseName = _unitOfWork.Courses.GetCourseName(session.CourseId),
                StartDate = session.StartDate,
                EndDate = session.EndDate
            };
        }


        public void Add(CreateSessionVM vm)
        {
            Session session = new Session
            {
                CourseId = vm.CourseId, 
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
       
            };

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Save();
        }
        public EditSessionVM? GetForEdit(int id)
        {
            Session session = _unitOfWork.Sessions.GetById(id);
            if (session == null) return null;

            return new EditSessionVM
            {
                Id = session.Id,
                CourseId = session.CourseId,
                StartDate = session.StartDate,
                EndDate = session.EndDate
            };
        }

        public void Update(EditSessionVM vm)
        {
            Session session = _unitOfWork.Sessions.GetById(vm.Id);
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

        public PageResult<SessionVM> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var sessions = _unitOfWork.Sessions.GetAllWithPagination(pageNumber, pageSize)
                .Select(s => new SessionVM
                {
                    Id = s.Id,
                    CourseName = _unitOfWork.Courses.GetCourseName(s.CourseId),
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                }).ToList();

            return new PageResult<SessionVM>
            {
                Items = sessions,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _unitOfWork.Sessions.GetTotalCount()
            };
        }

        public IEnumerable<SessionVM> GetByCourseName(string courseName)
        {
            return _unitOfWork.Sessions.GetSessionsByCourseName(courseName)
                .Select(s => new SessionVM
                {
                    Id = s.Id,
                    CourseName = _unitOfWork.Courses.GetCourseName(s.CourseId),
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                }).ToList();
        }
    }
}
