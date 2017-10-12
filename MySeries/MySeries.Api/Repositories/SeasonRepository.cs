using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MySeries.Api.Dto;
using MySeries.Api.EF;
using MySeries.Api.GlobalHandlers.Exceptions;
using MySeries.Api.Model;
using Newtonsoft.Json;

namespace MySeries.Api.Repositories
{
    public class SeasonRepository
    {
        private readonly MySeriesDbContext context;

        public SeasonRepository( MySeriesDbContext context )
        {
            this.context = context;
        }

        public Season GetSeason( object id )
        {
            var season = this.context.Seasons.Find( id );

            if (season == null)
            {
                throw new EntityNotFoundException( JsonConvert.SerializeObject( id ) );
            }

            this.context.Entry( season ).Collection( t => t.Episodes ).Query().OrderBy( c => c.EpisodeNumber ).Load();

            return season;
        }

        //public async Task<List<EpisodeDto>> GetEpisodes( int seasonId )
        //{
        //    var episodes = await this.context.Seasons.Episodes.Where( e => e.SeasonId == seasonId ).ToListAsync();
        //    var episodesDto = episodes.Select( e => e.ToDto() ).ToList();

        //    return episodesDto;
        //}


        public void InsertOrUpdateSeason( Season entity )
        {
            if (this.context.Seasons.Any( e => e.TmdbId == entity.TmdbId ))
            {
                this.context.Seasons.Attach( entity );
                this.context.Entry( entity ).State = EntityState.Modified;
            }
            else
            {
                this.context.Seasons.Add( entity );
            }
        }
    }
}