using System;
using System.Net;

namespace MySeries.Api.GlobalHandlers
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}