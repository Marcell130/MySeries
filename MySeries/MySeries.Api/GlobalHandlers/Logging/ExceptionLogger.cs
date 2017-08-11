using System;
using System.Collections;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
			int i = 0;
			while( exception != null )
			{
				WriteException( exception, i );
				exception = exception.InnerException;
				i++;
			}

			
		}

		private void WriteException(Exception exception, int level)
		{
			if( exception is BusinessLogicException )
			{
				Logger.Warn( $"{exception.Message}" );
			}
			else
			{
				if( exception is DbEntityValidationException validationException )
				{
					Logger.Error( validationException.EntityValidationErrors.Select( e => String.Join( "; ", e.ValidationErrors.Select( ve => $"{e.Entry.Entity.GetType().Name}.{ve.PropertyName}: {ve.ErrorMessage}" ) ) ) );
				}
				else
				{
					Logger.Error( $"{GetTab( level )}Type: {exception.GetType().FullName}" );
					Logger.Error( $"{GetTab( level )}Message: {exception.Message}" );
					Logger.Error( $"{GetTab( level )}Source: {exception.Source}" );
					Logger.Error( $"{GetTab( level )}TargetSite: {exception.TargetSite}" );
					Logger.Error( $"{GetTab( level )}Data: {GetExceptionData( exception )}" );
					Logger.Error( $"{GetTab( level )}StackTrace: {exception.StackTrace}" );
				}
			}
		}

		private string GetTab( int n )
		{
			return new String( '\t', n );
		}

		private string GetExceptionData( Exception ex )
		{
			var sb = new StringBuilder();
			foreach( DictionaryEntry entry in ex.Data )
			{
				sb.AppendLine( $"{entry.Key}: {entry.Value}; " );
			}
			return sb.ToString();
		}
	}
}