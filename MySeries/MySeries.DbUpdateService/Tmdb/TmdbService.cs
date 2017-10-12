using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySeries.DbUpdateService.Tmdb.Model;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace MySeries.DbUpdateService.Tmdb
{
    public class TmdbService
    {
        private readonly Uri tmdbApiBaseAddress;
        private readonly string tmdbApiKey;

        public TmdbService( Uri tmdbApiBaseAddress, string tmdbApiKey )
        {
            this.tmdbApiBaseAddress = tmdbApiBaseAddress;
            this.tmdbApiKey = tmdbApiKey;
        }

        //http://api.themoviedb.org/3/tv/popular?api_key=9b2c424d2882843814ecaaf6dccf5448

        public async Task<List<TmdbChange>> GetSeriesChanges( int seriesId )
        {
            var client = new RestClient( this.tmdbApiBaseAddress );

            var request = new RestRequest( $"tv/{seriesId}/changes", Method.GET );

            request.AddParameter( "api_key", this.tmdbApiKey, ParameterType.QueryString );

            request.AddParameter( "Accept", "application/json", ParameterType.HttpHeader );

            var response = await client.Execute<TmdbChanges>( request );

            var changes = response.Data.changes.ToList();
            return changes;
        }

        public async Task<List<TmdbChange>> GetSeasonChanges( int seasonId )
        {
            var client = new RestClient( this.tmdbApiBaseAddress );

            var request = new RestRequest( $"tv/season/{seasonId}/changes", Method.GET );

            request.AddParameter( "api_key", this.tmdbApiKey, ParameterType.QueryString );

            request.AddParameter( "Accept", "application/json", ParameterType.HttpHeader );

            var response = await client.Execute<TmdbChanges>( request );

            var changes = response.Data.changes.ToList();
            return changes;
        }

        public async Task<List<TmdbChange>> GetEpisodeChanges( int episodeId )
        {
            var client = new RestClient( this.tmdbApiBaseAddress );

            var request = new RestRequest( $"tv/episode/{episodeId}/changes", Method.GET );

            request.AddParameter( "api_key", this.tmdbApiKey, ParameterType.QueryString );

            request.AddParameter( "Accept", "application/json", ParameterType.HttpHeader );

            var response = await client.Execute<TmdbChanges>( request );

            var changes = response.Data.changes.ToList();
            return changes;
        }
    }
}
