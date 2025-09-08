using Graduation_Project.BLL.Services.Interfaces;
using Graduation_Project.DAl.Models;
using Graduation_Project.DAl.Repositories.GradeRepo;
using System.Collections.Generic;

namespace Graduation_Project.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public IEnumerable<Grade> GetAll() => _gradeRepository.GetAll();

        public Grade GetById(int id) => _gradeRepository.GetById(id);

        public void Create(Grade grade) => _gradeRepository.Add(grade);

        public void Update(Grade grade) => _gradeRepository.Update(grade);

        public void Delete(int id) => _gradeRepository.Delete(id);

        public IEnumerable<Grade> GetGradesByTraineeId(int traineeId) =>
            _gradeRepository.GetGradesByTraineeId(traineeId);
    }
}
