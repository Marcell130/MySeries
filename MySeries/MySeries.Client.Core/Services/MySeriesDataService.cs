using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MySeries.Client.Core.Helpers;
using MySeries.Client.Core.Model;
using MySeries.Client.Core.Model.BindingModels;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace MySeries.Client.Core.Services
{
    public class MySeriesDataService : IMySeriesDataService
    {
        public async Task<Token> TryGetTokenAsync( string username, string password )
        {
            using (var client = new HttpClient { BaseAddress = new Uri( Configuration.MySeriesApiBaseAddress ) })
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
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var token = JsonConvert.DeserializeObject<Token>( await response.Content.ReadAsStringAsync() );

                return token;
            }
        }

        public async Task SignupAsync( SignupBindingModel model )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( "/api/account/create", Method.POST );

            request.AddParameter( "application/json; charset=utf-8", JsonConvert.SerializeObject( model ), ParameterType.RequestBody );

            var response = await client.Execute( request );

            //TODO error handling
        }

        public async Task<UserInfo> GetUserInfoAsync( Token token )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( "/api/Me", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<UserInfo>( request );
            //TODO error handling

            return response.Data;
        }

        public async Task<List<TvShow>> GetAllSeriesAsync( Token token, int page, int perPage )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( "/api/TvShows", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );
            request.AddParameter( "page", page, ParameterType.QueryString );
            request.AddParameter( "perPage", perPage, ParameterType.QueryString );

            var response = await client.Execute<List<TvShow>>( request );
            //TODO error handling

            return response.Data;
        }

        public async Task<TvShow> GetSeriesDetailsAsync( Token token, int tvShowId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( $"/api/TvShows/{tvShowId}", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<TvShow>( request );

            if (!response.IsSuccess)
            {
                HandleError( response );
            }

            return response.Data;
        }

        public async Task<Season> GetSeasonDetailsAsync( Token token, int seasonId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( $"/api/Seasons/{seasonId}", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<Season>( request );

            if (!response.IsSuccess)
            {
                HandleError( response );
            }

            return response.Data;
        }

        public async Task<Episode> GetEpisodeDetailsAsync( Token token, int episodeId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( $"/api/Episodes/{episodeId}", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<Episode>( request );

            if (!response.IsSuccess)
            {
                HandleError( response );
            }

            return response.Data;
        }





































        public async Task<List<TvShow>> GetUserSeriesAsync( Token token )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( "/api/Me/TvShows", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<List<TvShow>>( request );
            //TODO error handling

            return response.Data;
        }



        public async Task<List<Episode>> GetEpisodesInSeasonAsync( Token token, int seasonId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( $"/api/Seasons/{seasonId}/Episodes", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<List<Episode>>( request );
            //TODO error handling

            return response.Data;
        }

        public async Task AddTvShowForUserAsync( Token token, int tvShowId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( $"/api/Me/TvShows/{tvShowId}", Method.POST );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<List<Episode>>( request );

            if (!response.IsSuccess)
            {
                HandleError( response );
            }
        }

        public async Task RemoveTvShowForUserAsync( Token token, int tvShowId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( $"/api/Me/TvShows/{tvShowId}", Method.DELETE );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<List<Episode>>( request );
            //TODO error handling

        }

        public async Task<bool> IsTvShowForUserAsync( Token token, int tvShowId )
        {
            var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

            var request = new RestRequest( $"/api/Me/TvShows/{tvShowId}", Method.GET );

            request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

            var response = await client.Execute<bool>( request );
            //TODO error handling

            return response.Data;
        }



        public async Task<List<Episode>> GetEpisodesInInterval( Token token, DateTime startDate, DateTime endDate )
        {
            try
            {

                var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

                var request = new RestRequest( "/api/Me/Episodes", Method.GET );

                request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

                request.AddParameter( "startDate", startDate, ParameterType.QueryString );
                request.AddParameter( "endDate", endDate, ParameterType.QueryString );

                var response = await client.Execute<List<Episode>>( request );
                //TODO error handling

                return response.Data;
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }

        //public async Task<List<Season>> GetSeasonsInTvShowAsync( Token token, int tvShowId )
        //{
        //    var client = new RestClient( Configuration.MySeriesApiBaseAddress ) { IgnoreResponseStatusCode = true };

        //    var request = new RestRequest( $"/api/tvShows/{tvShowId}/Seasons", Method.GET );

        //    request.AddHeader( "Authorization", $"{token.TokenType} {token.AccessToken}" );

        //    var response = await client.Execute<List<Season>>( request );
        //    //TODO error handling

        //    return response.Data;
        //}

        private void HandleError( IRestResponse response )
        {
            //TODO handle errors 
        }
    }
}
