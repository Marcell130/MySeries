using System.Web.Http.ExceptionHandling;

namespace MySeries.Api.GlobalHandlers
{
    public class ApplicationExceptionHandler : ExceptionHandler
    {
        public override void Handle( ExceptionHandlerContext context )
        {
            context.Result = new ErrorResult( context.Exception, context.Request );
        }
    }
}