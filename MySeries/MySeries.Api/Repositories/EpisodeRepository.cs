using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.EF;
using MySeries.Api.Model;
using System.Data.Entity;
using System.Threading.Tasks;
using MySeries.Api.Dto;
using MySeries.Api.GlobalHandlers.Exceptions;
using Newtonsoft.Json;


namespace MySeries.Api.Repositories
{
    public class EpisodeRepository
    {
        private readonly MySeriesDbContext context;

        public EpisodeRepository( MySeriesDbContext context )
        {
            this.context = context;
        }

        public Episode GetEpisode( object id )
        {
            var episode = this.context.Episodes.Find( id );

            if (episode == null)
            {
                throw new EntityNotFoundException( JsonConvert.SerializeObject( id ) );
            }
            
            return episode;
        }

        public async Task<EpisodeDto> GetNextEpisode( TvShow tvShow )
        {
            var episode = await this.context.Episodes
                                            .Include( e => e.Season )
                                            .Include( e => e.Season.TvShow )
                                            .Where( e => e.Season.TvShow.TmdbId == tvShow.TmdbId )
                                            .OrderBy( e => e.AirDate )
                                            .FirstOrDefaultAsync( e => DateTime.Today <= e.AirDate );
            return episode?.ToDto();
        }

        public void InsertOrUpdate( Episode entity )
        {
            if (this.context.Episodes.Any( e => e.TmdbId == entity.TmdbId ))
            {
                this.context.Episodes.Attach( entity );
                this.context.Entry( entity ).State = EntityState.Modified;
            }
            else
            {
                this.context.Episodes.Add( entity );
            }
        }
    }
}