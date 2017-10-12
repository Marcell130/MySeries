using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySeries.Api.Model
{
    public class Season
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.None )]
        public int TmdbId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }

        [StringLength( 32 )]
        public string PosterUriPostfix { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength( 3000 )]
        public string Overview { get; set; }

        [Column( TypeName = "Date" )]
        public DateTime? AirDate { get; set; }

        public List<Episode> Episodes { get; set; }

        [ForeignKey( "TvShowId" )]
        public TvShow TvShow { get; set; }
        public int TvShowId { get; set; }
    }
}
