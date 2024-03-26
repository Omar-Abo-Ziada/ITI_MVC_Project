using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InstitueProject.Filters
{
    public class HandleErrorAttribute : Attribute, IExceptionFilter //IFilterMetadata
    {
        public void OnException(ExceptionContext context)
        {
            ViewResult viewResult = new ViewResult();
            viewResult.ViewName = "Error";
            context.Result = viewResult; 
        }
    }
}
