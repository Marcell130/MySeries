using System;

namespace MySeries.UniversalClient
{
	public static class Configuration
	{
        public static Uri MySeriesApiBaseAddress => new Uri( "http://myseriesapi.azurewebsites.net/" );
        //public static Uri MySeriesApiBaseAddress => new Uri( "http://localhost:10032/" ); //TODO

    }
}
