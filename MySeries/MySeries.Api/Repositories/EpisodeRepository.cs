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

        public async Task<EpisodeDetailedDto> GetEpisode( int episodeId )
        {
            var episode = await this.context.Episodes/*.Include( e => e.Comments )*/
                                                     .Include( e => e.Comments.Select( c => c.User ) )
                                                     .SingleOrDefaultAsync( e => e.TmdbId == episodeId );

            if (episode == null)
            {
                throw new EntityNotFoundException( episodeId );
            }

            return episode.ToDetailedDto();
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

        public void AddComment( string userId, int episodeId, string text )
        {
            var comment = new EpisodeComment
            {
                UserId = userId,
                EpisodeId = episodeId,
                Text = text,
                Timestamp = DateTime.Now
            };

            this.context.EpisodeComments.Add( comment );
        }

    }
}