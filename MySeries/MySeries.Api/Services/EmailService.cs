

using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MySeries.Api.Services
{
	public class EmailService : IIdentityMessageService
	{
		public async Task SendAsync( IdentityMessage message )
		{
			var from = new EmailAddress( "gyory.marcell@gmail.com", "MyService Admin" );
			var subject = message.Subject;
			var to = new EmailAddress( message.Destination );
			var plainTextContent = message.Body;
			var htmlContent = message.Body;
			var msg = MailHelper.CreateSingleEmail( from, to, subject, plainTextContent, htmlContent );

			var apiKey = Configuration.SendGridApiKey;
			var client = new SendGridClient( apiKey );
			var response = await client.SendEmailAsync( msg );
			
			//TODO: handle and log if response contains error code

		}
	}
}