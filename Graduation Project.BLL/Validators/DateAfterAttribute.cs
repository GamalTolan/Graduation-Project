using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation_Project.BLL.Validators
{
    public class DateAfterAttribute : ValidationAttribute
    {
        private readonly string _startDateProperty;

        public DateAfterAttribute(string startDateProperty)
        {
            _startDateProperty = startDateProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var startDateProp = validationContext.ObjectType.GetProperty(_startDateProperty);
            if (startDateProp == null)
                return ValidationResult.Success;

            var startDate = startDateProp.GetValue(validationContext.ObjectInstance) as DateTime?;
            var endDate = value as DateTime?;

            if (startDate.HasValue && endDate.HasValue && endDate.Value <= startDate.Value)
                return new ValidationResult("End Date must be after Start Date.");

            return ValidationResult.Success;

        }
    }  
}
