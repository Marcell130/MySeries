using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySeries.Api.Model
{
    public class TvShow
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.None )]
        public int TmdbId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength( 32 )]
        public string WallpaperUriPostfix { get; set; }

        [StringLength( 32 )]
        public string PosterUriPostfix { get; set; }
        
        public short SeasonCount { get; set; }

        [StringLength( 3000 )]
        public string Overview { get; set; }

        public List<Genre> Genres { get; set; }
        public bool InProduction { get; set; }
        //public double Popularity { get; set; }
        //public double VoteAverage { get; set; }
        //public int VoteCount { get; set; }

        [StringLength( 20 )]
        public string Status { get; set; }

        [StringLength( 20 )]
        public string Type { get; set; }

        public short NumberOfEpisodes { get; set; }

        [Column( TypeName = "Date" )]
        public DateTime? FirstAirDate { get; set; }

        [Column( TypeName = "Date" )]
        public DateTime? LastAirDate { get; set; }

        public List<Season> Seasons { get; set; }
        public List<UserTvShow> UsersTvShows { get; set; }

        public TvShowState State { get; set; }
    }
}
