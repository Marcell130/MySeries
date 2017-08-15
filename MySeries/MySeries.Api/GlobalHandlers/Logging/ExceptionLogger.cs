using System.Web.Http.ExceptionHandling;
using log4net;

namespace MySeries.Api.GlobalHandlers.Logging
{
    public class SurveyAdminExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        public override void Log( ExceptionLoggerContext context )
        {
            var exception = context.Exception;
            var exceptionString = exception.GetExceptionString();
            Logger.Error( exceptionString );
        }
    }
}