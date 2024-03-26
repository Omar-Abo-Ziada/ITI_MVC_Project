using Microsoft.AspNetCore.Mvc.Filters;

namespace InstitueProject.Filters
{
    public class ValidationAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // empty
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // i can check on model state or what ever i want ...
        }
    }
}
