using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.GradeVM
{
    public class GradeDetailsVM : BaseGradeVM
    {
        public int Id { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
    }
}

