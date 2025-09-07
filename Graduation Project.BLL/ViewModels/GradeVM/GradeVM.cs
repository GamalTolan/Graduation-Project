using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.GradeVM
{
    public class GradeVM
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int SessionId { get; set; }
        public int TraineeId { get; set; }
        public string TraineeName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string SessionName { get; set; } = string.Empty;
    }
}
