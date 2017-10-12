using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySeries.DbCopyService.Model
{
    public class Episode
    {
        public int TmdbId { get; set; }

        public DateTime? AirDate { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterUriPostfix { get; set; }
        public int SeasonId { get; set; }


        //TODO
        public float VoteAverage { get; set; }
        public int VoteCount { get; set; }
        //public int SeasonNumber { get; set; }
        //public string ProductionCode { get; set; }
    }
}
