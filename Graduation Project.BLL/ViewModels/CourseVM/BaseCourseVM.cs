using Graduation_Project.BLL.Validators;
using Graduation_Project.DAl.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.CourseVM
{
    public class BaseCourseVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be 3–50 characters")]
        //[Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id")]
        [NoNumber]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }

        [Required, DataType(DataType.Date)]
        [FutureDate]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required, DataType(DataType.Date)]
        [DateAfter(nameof(StartDate))]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }


        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Instructor is required")]
        public int InstructorId { get; set; }

    }
}
