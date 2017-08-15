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
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }

        public string PosterUri { get; set; }

        public List<EpisodeDto> Episodes { get; set; }
    }

    public static class SeasonExtension
    {
        public static SeasonDto ToDto( this Season season )
        {
            var seasonDto = new SeasonDto
            {
                Id = season.Id,
                EpisodeCount = season.EpisodeCount,
                SeasonNumber = season.SeasonNumber,
                PosterUri = season.PosterUri,
                Episodes = season.Episodes?.Select( e => e.ToDto() ).ToList()
            };
            return seasonDto;
        }
    }
}