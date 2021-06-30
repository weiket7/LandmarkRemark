using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LandmarkRemark.API.Utilities
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(CreateResponse(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }

        private static ApplicationResponse CreateResponse(ModelStateDictionary modelState)
        {
            var result = new ValidationResultModel(modelState);
            return ApplicationResponse.Error(result.Message, result.Errors);
        }
    }
}