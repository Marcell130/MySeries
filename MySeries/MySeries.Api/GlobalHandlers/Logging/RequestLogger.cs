using System.Net;
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

			var requestMessage = await request.Content.ReadAsByteArrayAsync();
			Log.Trace( $"Request: {requestMessage}" );

			var response = await base.SendAsync( request, cancellationToken );

			var responseMessage = await response.Content.ReadAsByteArrayAsync();
			Log.Trace( $"Response: {responseMessage}" );

			return response;
		}
	}
}