using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySeries.DbCopyService
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

            if (!TimeSpan.TryParse( ConfigurationManager.AppSettings["RunInterval"], out RunInterval ))
            {
                RunInterval = new TimeSpan( 1, 0, 0, 0 );
            }


        }



    }
}
