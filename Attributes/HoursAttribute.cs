using InstitueProject.Context;
using InstitueProject.Models;
using System.ComponentModel.DataAnnotations;

namespace InstitueProject.Attributes
{
    public class HoursAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ITIContext context = new ITIContext();

            if(value != null && (int)value != 0)
            {
                int myHours = (int)value;
                if (myHours % 3 == 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Hours must be dividable by 3");
                }
            }
            else
            {
                return new ValidationResult("the hours field is required");
            }

            //string hours = value as string;

            //if (hours == null)
            //{
            //    return new ValidationResult("null obj");

            //}
            //else
            //{
            //    if (int.TryParse(hours, out int numHours))
            //    {
            //        if (numHours % 3 == 0)
            //        {
            //            return ValidationResult.Success;
            //        }
            //        else
            //        {
            //            return new ValidationResult("Hours must be dividable by 3");
            //        }
            //    }
            //}

            //return base.IsValid(value, validationContext);
        }
    }
}
