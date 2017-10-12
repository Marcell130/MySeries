using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySeries.DbCopyService.Model
{
    public class Season
    {
        public int TmdbId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }
        public string PosterUriPostfix { get; set; }
        public List<Episode> Episodes { get; set; }
        public string Overview { get; set; }
        public DateTime? AirDate { get; set; }
        public string Title { get; set; }
        public int TvShowId { get; set; }
    }
}
