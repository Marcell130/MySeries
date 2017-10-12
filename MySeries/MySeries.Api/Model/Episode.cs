using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySeries.Api.Model
{
    public class Episode
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.None )]
        public int TmdbId { get; set; }

        [Column( TypeName = "Date" )]
        public DateTime? AirDate { get; set; }

        public short EpisodeNumber { get; set; }

        [StringLength( 200 )]
        public string Title { get; set; }

        [StringLength( 1000 )]
        public string Overview { get; set; }

        [StringLength( 32 )]
        public string PosterUriPostfix { get; set; }

        [ForeignKey( "SeasonId" )]
        public Season Season { get; set; }
        public int SeasonId { get; set; }

        //public float VoteAverage { get; set; }
        //public int VoteCount { get; set; }
    }
}
