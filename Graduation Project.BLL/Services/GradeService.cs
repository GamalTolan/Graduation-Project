using Graduation_Project.BLL.Pagination;
using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.GradeVM;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Graduation_Project.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(BaseGradeVM vm)
        {
            Grade grade = new Grade
            {
                TraineeId = vm.TraineeId,
                SessionId = vm.SessionId,
                Value = vm.Value
            };

            _unitOfWork.Grades.Add(grade);
            _unitOfWork.Save();
        }

        public void Update(GradeDetailsVM vm)
        {
            Grade grade = _unitOfWork.Grades.GetById(vm.Id);
            if (grade != null)
            {
                grade.Value = vm.Value;
                grade.SessionId = vm.SessionId;
                grade.TraineeId = vm.TraineeId;

                _unitOfWork.Grades.Update(grade);
                _unitOfWork.Save();
            }
        }


        public void Delete(int id)
        {
            _unitOfWork.Grades.Delete(id);
            _unitOfWork.Save();
        }


        public PageResult<GradeVM> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var grades = _unitOfWork.Grades.GetAllWithPagination(pageNumber, pageSize)
                .Select(g => new GradeVM
                {
                    Id = g.Id,
                    Value = g.Value,
                    SessionId = g.SessionId,
                    TraineeId = g.TraineeId,
                    TraineeName = _unitOfWork.Users.GetTraineeNameById(g.TraineeId) ?? "N/A",
                    CourseName = _unitOfWork.Grades.GetCourseNameBySessionId(g.SessionId) ?? "N/A",
                    SessionName = $"Session {g.SessionId}"

                }).ToList();

            return new PageResult<GradeVM>
            {
                Items = grades,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _unitOfWork.Grades.GetTotalCount()
            };
        }


        public PageResult<GradeVM> GetGradesByTrainee(int traineeId, int pageNumber, int pageSize)
        {
            var grades = _unitOfWork.Grades.GetGradesByTraineeId(traineeId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new GradeVM
                {
                    Id = g.Id,
                    Value = g.Value,
                    SessionId = g.SessionId,
                    TraineeId = g.TraineeId,
                    TraineeName = _unitOfWork.Users.GetTraineeNameById(g.TraineeId) ?? "N/A",
                    CourseName = g.Session?.Course?.Name ?? "N/A",
                    SessionName = $"Session {g.SessionId}"

                }).ToList();

            return new PageResult<GradeVM>
            {
                Items = grades,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = _unitOfWork.Grades.GetGradesByTraineeId(traineeId).Count()
            };
        }


        public GradeDetailsVM? GetById(int id)
        {
            Grade grade = _unitOfWork.Grades.GetById(id);
            if (grade == null) return null;

            return new GradeDetailsVM
            {
                Id = grade.Id,
                Value = grade.Value,
                TraineeId = grade.TraineeId,
                SessionId = grade.SessionId,
                TraineeName = _unitOfWork.Users.GetTraineeNameById(grade.TraineeId) ?? "N/A",
                CourseName = grade.Session?.Course?.Name ?? "N/A",
                SessionName = $"Session {grade.SessionId}",
                SessionStart = grade.Session?.StartDate ?? default,
                SessionEnd = grade.Session?.EndDate ?? default
            };
        }


        public bool IsGradeExists(int traineeId, int sessionId)
        {
            return _unitOfWork.Grades.GetAll()
                .Any(g => g.TraineeId == traineeId && g.SessionId == sessionId);
        }
    }
}
