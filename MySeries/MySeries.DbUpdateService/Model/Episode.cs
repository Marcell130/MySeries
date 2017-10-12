using System;

namespace MySeries.DbUpdateService.Model
{
    public class Episode
    {
        public int TmdbId { get; set; }

        public DateTime? AirDate { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterUriPostfix { get; set; }


        //TODO
        public float VoteAverage { get; set; }
        public int VoteCount { get; set; }
        //public int SeasonNumber { get; set; }
        //public string ProductionCode { get; set; }
    }
}
