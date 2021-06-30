using Microsoft.AspNetCore.Mvc.Filters;

namespace LandmarkRemark.API.Utilities
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        //reference: https://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}
