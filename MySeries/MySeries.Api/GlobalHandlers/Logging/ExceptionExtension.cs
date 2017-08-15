using System;
using System.Collections;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using MySeries.Api.GlobalHandlers.Exceptions;

namespace MySeries.Api.GlobalHandlers.Logging
{
    public static class ExceptionExtension
    {
        public static string GetExceptionString( this Exception exception )
        {
            var builder = new StringBuilder();
            int i = 0;
            while( exception != null )
            {
                builder = WriteException( exception, i, builder );
                exception = exception.InnerException;
                i++;
            }
            return builder.ToString();
        }


        private static StringBuilder WriteException( Exception exception, int level, StringBuilder builder )
        {
            if( exception is BusinessLogicException )
            {
                builder.AppendLine( $"{exception.Message}" );
            }
            else
            {
                var entityValidationException = exception as DbEntityValidationException;
                if( entityValidationException != null )
                {
                    var validationException = entityValidationException;
                    var validationErrors = validationException.EntityValidationErrors.Select( e => String.Join( "; ", e.ValidationErrors.Select( ve => $"{e.Entry.Entity.GetType().Name}.{ve.PropertyName}: {ve.ErrorMessage}" ) ) ).ToList();
                    foreach( var validationError in validationErrors )
                    {
                        builder.AppendLine( validationError );
                    }
                }
                else
                {
                    builder.AppendLine( $"{GetTab( level )}Type: {exception.GetType().FullName}" );
                    builder.AppendLine( $"{GetTab( level )}Message: {exception.Message}" );
                    builder.AppendLine( $"{GetTab( level )}Source: {exception.Source}" );
                    builder.AppendLine( $"{GetTab( level )}TargetSite: {exception.TargetSite}" );
                    builder.AppendLine( $"{GetTab( level )}Data: {GetExceptionData( exception )}" );
                    builder.AppendLine( $"{GetTab( level )}StackTrace: {exception.StackTrace}" );
                }
            }

            return builder;
        }

        private static string GetTab( int n )
        {
            return new String( '\t', n );
        }

        private static string GetExceptionData( Exception ex )
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