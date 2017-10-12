using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MySeries.DbCopyService.Model;
using MySeries.DbCopyService.Tmdb.Model;
using RestSharp;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace MySeries.DbCopyService.MySeries
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
            using( var client = new HttpClient { BaseAddress = mySeriesApiBaseAddress } )
            {
                var request = new HttpRequestMessage( HttpMethod.Post, "/token" );

                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", username ),
                    new KeyValuePair<string, string>( "password", password )
                };
                request.Content = new FormUrlEncodedContent( formData );

                var response = await client.SendAsync( request );

                var token = await response.Content.ReadAsAsync<Token>();

                if (token.AccessToken == null)
                {
                    throw new Exception("Could not retrive token from MySeriesApi");
                }

                return token;
            }
        }

        public async Task AddTvShow( TvShow tvShow, Token token )
        {
            using( var client = new HttpClient { BaseAddress = this.mySeriesApiBaseAddress, Timeout = new TimeSpan( 0, 15, 0 ) } )
            {
                client.DefaultRequestHeaders.Add( "Authorization", $"{token.TokenType} {token.AccessToken}" );

                var response = await client.PostAsJsonAsync( "/api/TvShows", tvShow );

                if( !response.IsSuccessStatusCode )
                {
                    if( response.Content != null )
                    {
                        throw new Exception( $"Server error {response.StatusCode} - {await response.Content.ReadAsStringAsync()}" );
                    }
                    throw new Exception( $"Server error {response.StatusCode}" );
                }
            }
        }

        public async Task<List<int>> GetTvShowTmdbIds( Token token )
        {
            using (var client = new HttpClient { BaseAddress = this.mySeriesApiBaseAddress, Timeout = new TimeSpan( 0, 15, 0 ) })
            {
                client.DefaultRequestHeaders.Add( "Authorization", $"{token.TokenType} {token.AccessToken}" );

                var response = await client.GetAsync( "/api/TvShows/TmdbIds" );

                if (!response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        throw new Exception( $"Server error {response.StatusCode} - {await response.Content.ReadAsStringAsync()}" );
                    }
                    throw new Exception( $"Server error {response.StatusCode}" );
                }

                return await response.Content.ReadAsAsync<List<int>>();
            }
        }
    }
}
