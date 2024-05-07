using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using System.Net;

namespace TFEHelper.Backend.API.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private BadRequestObjectResult CreateBadRequestObjectResult(FilterContext context)
        {
            return new BadRequestObjectResult(new APIResponseDTO()
            {
                IsSuccessful = false,
                Payload = null,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = context.ModelState.Keys.SelectMany(key => context.ModelState[key]!.Errors.Select(x => key + ": " + x.ErrorMessage)).ToList()
            });
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) context.Result = CreateBadRequestObjectResult(context);
        }

        public void OnActionExecuted(ActionExecutedContext context) 
        {
            if (!context.ModelState.IsValid) context.Result = CreateBadRequestObjectResult(context);
        }
    }
}
