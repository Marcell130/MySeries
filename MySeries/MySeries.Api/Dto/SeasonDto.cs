using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.Model;

namespace MySeries.Api.Dto
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SeasonNumber { get; set; }
        public string PosterUriPostfix { get; set; }
    }

    public class SeasonDetailedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }

        public string PosterUriPostfix { get; set; }

        public List<EpisodeDto> Episodes { get; set; }
    }

    public static class SeasonExtension
    {
        public static SeasonDto ToDto( this Season season )
        {
            var seasonDto = new SeasonDto
            {
                Id = season.TmdbId,
                Title = season.Title,
                SeasonNumber = season.SeasonNumber,
                PosterUriPostfix = season.PosterUriPostfix,
            };
            return seasonDto;
        }

        public static SeasonDetailedDto ToDetailedDto( this Season season )
        {
            var seasonDto = new SeasonDetailedDto
            {
                Id = season.TmdbId,
                Title = season.Title,
                SeasonNumber = season.SeasonNumber,
                EpisodeCount = season.EpisodeCount,
                PosterUriPostfix = season.PosterUriPostfix,
                Episodes = season.Episodes?.Select( e => e.ToDto() ).ToList()
            };
            return seasonDto;
        }
    }
}