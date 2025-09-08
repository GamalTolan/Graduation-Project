using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.BLL.ViewModels.GradeVM;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories;
using System.Collections.Generic;
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

        public IEnumerable<GradeVM> GetAll()
        {
            var grades = _unitOfWork.Grades.GetAll();
            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId,
                SessionName = g.Session?.Course?.Name ?? "N/A",
                TraineeId = g.TraineeId,
                TraineeName = g.Trainee?.Name ?? "N/A"
            }).ToList();
        }

        public GradeDetailsVM? GetById(int id)
        {
            var grade = _unitOfWork.Grades.GetById(id);
            if (grade == null) return null;

            return new GradeDetailsVM
            {
                Id = grade.Id,
                Value = grade.Value,
                SessionId = grade.SessionId,
                SessionName = grade.Session?.Course?.Name ?? "N/A",
                TraineeId = grade.TraineeId,
                TraineeName = grade.Trainee?.Name ?? "N/A"
            };
        }

        public void Add(BaseGradeVM vm)
        {
            var grade = new Grade
            {
                Value = vm.Value,
                SessionId = vm.SessionId,
                TraineeId = vm.TraineeId
            };

            _unitOfWork.Grades.Add(grade);
            _unitOfWork.Save();
        }

        public void Update(GradeVM vm)
        {
            var grade = _unitOfWork.Grades.GetById(vm.Id);
            if (grade == null) return;

            grade.Value = vm.Value;
            grade.SessionId = vm.SessionId;
            grade.TraineeId = vm.TraineeId;

            _unitOfWork.Grades.Update(grade);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Grades.Delete(id);
            _unitOfWork.Save();
        }

        public GradeVM? GetForEdit(int id)
        {
            var grade = _unitOfWork.Grades.GetById(id);
            if (grade == null) return null;

            return new GradeVM
            {
                Id = grade.Id,
                Value = grade.Value,
                SessionId = grade.SessionId,
                TraineeId = grade.TraineeId
            };
        }
    }
}
