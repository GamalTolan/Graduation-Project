using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.Validators
{
    public class FutureDateAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                
                var idProperty = validationContext.ObjectType.GetProperty("Id");
                if (idProperty != null)
                {
                    var idValue = (int)idProperty.GetValue(validationContext.ObjectInstance)!;
                    if (idValue > 0) 
                    {
                        return ValidationResult.Success;
                    }
                }  
            }

                if (value is DateTime date && date < DateTime.Today)
                return new ValidationResult("Date cannot be in the past.");

            return ValidationResult.Success;
        }


    }
}
