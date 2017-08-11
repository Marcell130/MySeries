using System.Configuration;

namespace MySeries.Api
{
	public static class Configuration
	{
		public static string SendGridApiKey;
		
		static Configuration()
		{
			SendGridApiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
		}
	}
}
