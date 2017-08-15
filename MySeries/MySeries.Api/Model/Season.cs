using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySeries.Api.Model
{
    public class Season
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }

        public string PosterUri { get; set; }

        public List<Episode> Episodes { get; set; }

        [ForeignKey( "TvShowId" )]
        public TvShow TvShow { get; set; }
        public int TvShowId { get; set; }
    }
}
