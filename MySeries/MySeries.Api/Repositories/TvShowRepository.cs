using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MySeries.Api.EF;
using MySeries.Api.GlobalHandlers.Exceptions;
using MySeries.Api.Model;
using Newtonsoft.Json;

namespace MySeries.Api.Repositories
{
    public class TvShowRepository
    {
        private readonly MySeriesDbContext context;

        public TvShowRepository( MySeriesDbContext context )
        {
            this.context = context;
        }

        public TvShow GetTvShow( object id )
        {
            var tvShow = this.context.TvShows.Find( id );

            if (tvShow == null)
            {
                throw new EntityNotFoundException( JsonConvert.SerializeObject( id ) );
            }

            this.context.Entry( tvShow ).Collection( t => t.Seasons ).Load();

            return tvShow;
        }

        public List<TvShow> GetTvShows( Func<TvShow, string> orderBy, int page, int perPage )
        {
            return this.context.TvShows.OrderBy( orderBy ).Skip( (page - 1) * perPage ).Take( perPage ).ToList();
        }

        public async Task<List<TvShow>> SearchTitle( string title )
        {
            var titleLower = title.ToLower();
            var tvShows = await this.context.TvShows.Where( t => t.Title.ToLower().Contains( titleLower ) ).ToListAsync();

            return tvShows;
        }

        public async Task<List<int>> GetTmdbIds()
        {
            var tmdbIds = await this.context.TvShows.Select( t => t.TmdbId ).ToListAsync();
            return tmdbIds;
        }

        public void InsertOrUpdate( TvShow entity )
        {
            if (this.context.TvShows.Any( e => e.TmdbId == entity.TmdbId ))
            {
                this.context.TvShows.Attach( entity );
                this.context.Entry( entity ).State = EntityState.Modified;
            }
            else
            {
                this.context.TvShows.Add( entity );
            }
        }
    }
}