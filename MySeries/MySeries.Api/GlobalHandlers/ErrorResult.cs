using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MySeries.Api.GlobalHandlers.Exceptions;
using MySeries.Api.GlobalHandlers.Logging;

namespace MySeries.Api.GlobalHandlers
{
    public class ErrorResult : IHttpActionResult
    {
        private readonly Exception exception;

        private readonly HttpRequestMessage request;


        public ErrorResult( Exception exception, HttpRequestMessage request )
        {
            this.exception = exception;
            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync( CancellationToken cancellationToken )
        {
            return Task.FromResult( Execute() );
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response;
#if DEBUG
            response = this.request.CreateErrorResponse( HttpStatusCode.InternalServerError, this.exception.GetExceptionString() );
#else
            if (this.exception is BusinessLogicException)
            {
                var logicException = this.exception as BusinessLogicException;
                response = this.request.CreateErrorResponse(logicException.StatusCode, logicException.Message);
            }
            else if (this.exception is EntityNotFoundException)
            {
                response = this.request.CreateErrorResponse(HttpStatusCode.NotFound, this.exception.Message);
            }
            else if (this.exception is DbEntityValidationException)
            {
                response = this.request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request did not contain all the necessary information");
            }
            else if (this.exception is SqlException)
            {
                response = this.request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Database error occured");
            }
            else
            {
                response = this.request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error occured");
                response = this.request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error occured");
            }
#endif

            return response;
        }
    }
}