using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MySeries.DbUpdateService.Model;
using MySeries.DbUpdateService.Tmdb.Model;

namespace MySeries.DbUpdateService.MySeries
{
    public class MySeriesService
    {
        private readonly Uri mySeriesApiBaseAddress;

        public MySeriesService( Uri mySeriesApiBaseAddress )
        {
            this.mySeriesApiBaseAddress = mySeriesApiBaseAddress;
        }

        public async Task<Token> GetTokenAsync( string username, string password )
        {
            using (var client = new HttpClient { BaseAddress = this.mySeriesApiBaseAddress })
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


        public async Task UpdateTvShow( SeriesChanges tvShow, Token token )
        {
            using (var client = new HttpClient { BaseAddress = this.mySeriesApiBaseAddress, Timeout = new TimeSpan( 0, 15, 0 ) })
            {
                client.DefaultRequestHeaders.Add( "Authorization", $"{token.TokenType} {token.AccessToken}" );

                var response = await client.PutAsJsonAsync( "/api/TvShows", tvShow );

                if (!response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        throw new Exception( $"Server error {response.StatusCode} - {await response.Content.ReadAsStringAsync()}" );
                    }
                    throw new Exception( $"Server error {response.StatusCode}" );
                }
            }
        }
    }
}
