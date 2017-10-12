using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySeries.DbUpdateService.Tmdb.Model
{
    public class SeasonChangeValue
    {
        public int season_id { get; set; }
        public int season_number { get; set; }
    }


    public class EpisodeChangeValue
    {
        public int episode_id { get; set; }
        public int episode_number { get; set; }
    }



    public class PosterChangeValue
    {
        public ImagePageValue poster { get; set; }
    }

    public class ImagePageValue
    {
        public string file_path { get; set; }
        public string iso_639_1 { get; set; }
    }


}
