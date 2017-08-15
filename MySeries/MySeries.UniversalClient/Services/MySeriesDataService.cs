using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Model;
using MySeries.UniversalClient.Model.BindingModels;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace MySeries.UniversalClient.Services
{
    public static class MySeriesDataService
    {
        public static async Task<Token> LoginAsync( string username, string password )
        {
            using( var client = new HttpClient { BaseAddress = Configuration.MySeriesApiBaseAddress } )
            {
                var request = new HttpRequestMessage( HttpMethod.Post, "/token" );

                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", username ),
                    new KeyValuePair<string, string>( "password", password )
                };
                //formData.Add( new KeyValuePair<string, string>( "scope", "all" ) );
                request.Content = new FormUrlEncodedContent( formData );

                var response = await client.SendAsync( request );
                //TODO error handling

                var token = await response.Content.ReadAsAsync<Token>();

                return token;
            }
        }

        public static async Task SignupAsync( SignupBindingModel model )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress );

            var request = new RestRequest( "/api/account/create", Method.POST );

            request.AddParameter( "application/json; charset=utf-8", Json.StringifyAsync( model ), ParameterType.RequestBody );

            var response = await client.Execute( request );

            //TODO error handling
        }

        public static async Task<UserInfo> GetUserInfoAsync( Token token )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress );

            var request = new RestRequest( "/api/Me", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<UserInfo>( request );
            //TODO error handling

            return response.Data;
        }
        
        public static async Task<List<TvShow>> GetUserSeriesAsync( Token token )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress );

            var request = new RestRequest( "/api/Me/TvShows", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<List<TvShow>>( request );
            //TODO error handling

            return response.Data;
        }

        public static async Task<List<Episode>> GetEpisodesInSeasonAsync( Token token, int seasonId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress );

            var request = new RestRequest( $"/api/Seasons/{seasonId}/Episodes", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<List<Episode>>( request );
            //TODO error handling

            return response.Data;
        }
    }
}
