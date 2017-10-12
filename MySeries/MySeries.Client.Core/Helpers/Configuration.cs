using System.Linq;
using MySeries.Client.Core.Model;
using Xamarin.Auth;

namespace MySeries.Client.Core.Helpers
{
    public static class Configuration
    {
        //TODO replace to recources

        public const string MySeriesApiBaseAddress = "http://myseriesapi.azurewebsites.net/";
        public const string ApplicationName = "MySeries";



        public static void SaveCredentials( string userName, string password, Token token )
        {
            if (!string.IsNullOrWhiteSpace( userName ) && !string.IsNullOrWhiteSpace( password ))
            {
                Account account = new Account
                {
                    Username = userName,
                    Properties = { { "Token", token.AccessToken }, { "TokenType", token.TokenType } }
                };
                account.Properties.Add( "Password", password );
                AccountStore.Create().Save( account, ApplicationName );
            }
        }

        public static string UserName => AccountStore.Create().FindAccountsForService( ApplicationName ).FirstOrDefault()?.Username;

        public static string Password => AccountStore.Create().FindAccountsForService( ApplicationName ).FirstOrDefault()?.Properties["Password"];

        //public static Token MySeriesToken
        //{
        //    get
        //    {
        //        var account = AccountStore.Create().FindAccountsForService( ApplicationName ).FirstOrDefault();
        //        var accessToken = account?.Properties["AccessToken"];
        //        var tokenType = account?.Properties["TokenType"];
                
        //        if (accessToken != null)
        //        {
        //            return new Token {AccessToken = accessToken, TokenType = tokenType};
        //        }
        //        else
        //        {
                    
        //        }
        //    };
        //}
    }
}
