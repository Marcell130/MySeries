using System.Web.Http.ExceptionHandling;

namespace MySeries.Api.GlobalHandlers
{
    public class SurveyAdminExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ErrorResult(context.Exception, context.Request);
        }
    }
}