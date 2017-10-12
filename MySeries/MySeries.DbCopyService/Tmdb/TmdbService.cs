using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySeries.DbCopyService.Model;
using MySeries.DbCopyService.Tmdb.Model;
using RestSharp;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace MySeries.DbCopyService.Tmdb
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

        public async Task<List<int>> GetPopularIds( int page )
        {
            var client = new RestClient( this.tmdbApiBaseAddress );

            var request = new RestRequest( "tv/popular", Method.GET );

            request.AddParameter( "api_key", this.tmdbApiKey, ParameterType.QueryString );
            request.AddParameter( "page", page, ParameterType.QueryString );

            request.AddParameter( "Accept", "application/json", ParameterType.HttpHeader );

            var response = await client.Execute<PopularTvShows>( request );

            var tvShowIds = response.Data.results.Select( r => r.id ).ToList();
            return tvShowIds;
            //var tvShows = new List<TvShow>();
            //foreach( var tvShowId in tvShowIds )
            //{
            //    tvShows.Add( await GetTvShow( tvShowId ) );
            //}
            //return tvShows;
        }

        public async Task<TvShow> GetTvShow( int tvShowId )
        {
            var client = new RestClient( this.tmdbApiBaseAddress );

            var request = new RestRequest( $"tv/{tvShowId}", Method.GET );

            request.AddParameter( "api_key", this.tmdbApiKey, ParameterType.QueryString );

            var response = await client.Execute<TvShowDetails>( request );

            var tvShow = response.Data.ToTvShow();
            var seasonNumbers = response.Data.Seasons.Select( s => s.SeasonNumber ).ToList();

            foreach (var seasonNumber in seasonNumbers)
            {
                tvShow.Seasons.Add( await GetSeason( tvShowId, seasonNumber ) );
            }

            return tvShow;
        }


        private async Task<Season> GetSeason( int tvShowId, int seasonNumber )
        {
            var client = new RestClient( this.tmdbApiBaseAddress );

            var request = new RestRequest( $"tv/{tvShowId}/season/{seasonNumber}", Method.GET );

            request.AddParameter( "api_key", this.tmdbApiKey, ParameterType.QueryString );

            var response = await client.Execute<SeasonDetails>( request );

            return response.Data.ToSeason( tvShowId );
        }
    }
}
