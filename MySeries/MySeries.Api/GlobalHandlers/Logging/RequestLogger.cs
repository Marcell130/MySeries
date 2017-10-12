using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace MySeries.Api.GlobalHandlers.Logging
{
    public class RequestLogger : DelegatingHandler
    {
        private static readonly ILog Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            var requestPayload = request.Content == null ? "" : request.Content.ReadAsStringAsync().Result;
            Log.Trace( $"Request: {request.Method.Method} {request.RequestUri.LocalPath} - {requestPayload}" );

            var response = await base.SendAsync( request, cancellationToken );


            var responsePayload = response.Content == null ? "" : response.Content.ReadAsStringAsync().Result;
            Log.Trace( $"Response: {response.StatusCode} - {responsePayload}" );

            return response;
        }
    }
}