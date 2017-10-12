using System;
using System.Configuration;

namespace MySeries.Api
{
    public static class Configuration
    {
        public static string SendGridApiKey;

        public static string LuisBaseAddress;
        public static string LuisApiKey;
        public static string LuisAppId;


        static Configuration()
        {
            SendGridApiKey = ConfigurationManager.AppSettings["SendGridApiKey"];

            LuisBaseAddress = ConfigurationManager.AppSettings["LuisBaseAddress"];
            LuisApiKey = ConfigurationManager.AppSettings["LuisApiKey"];
            LuisAppId = ConfigurationManager.AppSettings["LuisAppId"];
        }
    }
}
