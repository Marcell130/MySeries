using System;
using System.Net;

namespace MySeries.Api.GlobalHandlers.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException( string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError ) : base( message )
        {
            StatusCode = statusCode;
        }

        public BusinessLogicException( string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.InternalServerError ) : base( message, innerException )
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}