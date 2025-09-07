using Graduation_Project.DAl.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.ViewModels.UserVM
{
    public class BaseUserVM
    {
        

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, EmailAddress]
        //[Remote("IsEmailUnique", "User", AdditionalFields = "Id", ErrorMessage = "Email already exists")]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }

    }
}
