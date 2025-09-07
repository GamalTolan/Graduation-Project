using McMaster.Extensions.CommandLineUtils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.Validators
{
    public class NoNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string strValue)
            {
                if (strValue.Any(char.IsDigit))
                {
                    return new ValidationResult("The field must not contain numbers.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
