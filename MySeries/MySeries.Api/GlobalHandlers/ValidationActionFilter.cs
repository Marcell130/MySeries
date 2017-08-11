using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MySeries.Api.GlobalHandlers
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                var errorMessage = modelState.Values.FirstOrDefault(v => v.Errors.Count != 0 && v.Errors.All(e => !String.IsNullOrWhiteSpace(e.ErrorMessage)))?.Errors.FirstOrDefault(e => !String.IsNullOrWhiteSpace(e.ErrorMessage))?.ErrorMessage ?? $"Invalid {modelState.Keys.LastOrDefault()}";
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

        }
    }
}