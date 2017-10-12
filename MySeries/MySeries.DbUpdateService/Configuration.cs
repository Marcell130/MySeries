using System;
using System.Configuration;

namespace MySeries.DbUpdateService
{
    public class Configuration
    {
        public static Uri TmdbApiBaseAddress;
        public static string TmdbApiKey;

        public static Uri MySeriesApiBaseAddress;
        public static string MySeriesUsername;
        public static string MySeriesPassword;

        public static TimeSpan RunInterval;

        static Configuration()
        {
            TmdbApiBaseAddress = new Uri( ConfigurationManager.AppSettings["TmdbApiBaseAddress"] );
            TmdbApiKey = ConfigurationManager.AppSettings["TmdbApiKey"];

            MySeriesApiBaseAddress = new Uri( ConfigurationManager.AppSettings["MySeriesApiBaseAddress"] );
            MySeriesUsername = ConfigurationManager.AppSettings["MySeriesUsername"];
            MySeriesPassword = ConfigurationManager.AppSettings["MySeriesPassword"];

            if( !TimeSpan.TryParse( ConfigurationManager.AppSettings["RunInterval"], out RunInterval ) )
            {
                RunInterval = new TimeSpan( 23, 59, 59 );
            }


        }



    }
}
