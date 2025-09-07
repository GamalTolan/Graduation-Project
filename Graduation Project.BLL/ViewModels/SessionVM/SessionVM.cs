using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.SessionVM
{
    public class SessionVM
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
