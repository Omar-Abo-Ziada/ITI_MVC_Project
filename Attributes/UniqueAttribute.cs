using InstitueProject.Models;
using System.ComponentModel.DataAnnotations;
using InstitueProject.Context;


namespace InstitueProject.Attributes
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ITIContext context = validationContext.GetService<ITIContext>();//new ITIContext();

            string? name = value as string;

            if (name == null)
            {
                return new ValidationResult("Required");
            }
            else
            {
                Course CrsFromReq = validationContext.ObjectInstance as Course;

                Course CrsFromDb = context.Courses
                                  .FirstOrDefault(c => c.Name == name);// && c.DepartmentId==CrsFromReq.DepartmentId);

                if (CrsFromDb == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Name Already exists in the data base !");
                }

            }
        }
    }
}
