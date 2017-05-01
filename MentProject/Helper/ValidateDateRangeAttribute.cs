using System;
using System.ComponentModel.DataAnnotations;

namespace MentProject.Models
{
    internal class ValidateDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value >= DateTime.Now.AddYears(-150)  && (DateTime)value <= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date can't be more than day today, and older 150 years.");
            }
        }
    }
}