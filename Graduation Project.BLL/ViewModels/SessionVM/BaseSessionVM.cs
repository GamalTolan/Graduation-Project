using Graduation_Project.BLL.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.SessionVM
{
    public class BaseSessionVM
    {
       

        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }


        [Required, DataType(DataType.Date)]
        [FutureDate]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required, DataType(DataType.Date)]
        [DateAfter(nameof(StartDate))]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

    }
}
