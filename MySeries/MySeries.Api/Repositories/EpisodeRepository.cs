using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MySeries.Api.Dto;
using MySeries.Api.EF;
using MySeries.Api.Model;

namespace MySeries.Api.Repositories
{
    public class SeasonRepository : GenericRepository<Season>
    {
        public SeasonRepository( MySeriesDbContext context ) : base( context )
        {
        }

        public async Task<List<EpisodeDto>> GetEpisodes( int seasonId )
        {
            var episodes = await this.Context.Episodes.Where( e => e.SeasonId == seasonId ).ToListAsync();
            var episodesDto = episodes.Select( e => e.ToDto() ).ToList();

            var ep = new List<EpisodeDto>
            {
                new EpisodeDto{Title = "Sample Title 1", EpisodeNumber = 1, Id = 1, AirDate = DateTime.Today.AddDays(-70), Overview = "The plot of the story so much text many wow"},
                new EpisodeDto{Title = "Sample Title 2", EpisodeNumber = 2, Id = 2, AirDate = DateTime.Today.AddDays(-60), Overview = "The plot of the story so much text many wow"},
                new EpisodeDto{Title = "Sample Title 3", EpisodeNumber = 3, Id = 3, AirDate = DateTime.Today.AddDays(-50), Overview = "The plot of the story so much text many wow"},
                new EpisodeDto{Title = "Sample Title 4", EpisodeNumber = 4, Id = 4, AirDate = DateTime.Today.AddDays(-40), Overview = "The plot of the story so much text many wow"},
                new EpisodeDto{Title = "Sample Title 5", EpisodeNumber = 5, Id = 5, AirDate = DateTime.Today.AddDays(-30), Overview = "The plot of the story so much text many wow"},
                new EpisodeDto{Title = "Sample Title 6", EpisodeNumber = 6, Id = 6, AirDate = DateTime.Today.AddDays(-20), Overview = "The plot of the story so much text many wow"},
                new EpisodeDto{Title = "Sample Title 7", EpisodeNumber = 7, Id = 7, AirDate = DateTime.Today.AddDays(-10), Overview = "The plot of the story so much text many wow"},
            };

            return ep;
        }
    }
}