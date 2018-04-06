using PruebaUnoAgioGlobal.Web.Exceptions.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace PruebaUnoAgioGlobal.Web.Filters
{
    public class ValidateRequestsActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var reasons = from kvp in context.ModelState
                          from e in kvp.Value.Errors
                          let k = kvp.Key
                          select e.ErrorMessage;

            var badRequestInfo = new BadRequestExceptionInfo() { Code = default(EventId).Id.ToString(), Reasons = reasons.ToList() };

            context.Result = new BadRequestObjectResult(badRequestInfo);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This filter doesn't do anything post action.
        }
    }
}
