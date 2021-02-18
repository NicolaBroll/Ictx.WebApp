using System.Linq;
using Ictx.WebApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ictx.WebApp.Api.Common
{
    public class ValidateModelControllerFilterr : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                // validation error object.
                context.Result = new BadRequestObjectResult(new ErrorResponse(context.ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(k => k.Key, v => v.Value.Errors.Select(x => x.ErrorMessage))));

            base.OnActionExecuting(context);
        }
    }
}