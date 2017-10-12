using System;
using System.Collections.Generic;
using MySeries.DbCopyService.Tmdb.Model;

namespace MySeries.DbCopyService.Model
{
    public class TvShow
    {
        public int TmdbId { get; set; }
        public string Title { get; set; }
        public string WallpaperUriPostfix { get; set; }
        public string PosterUriPostfix { get; set; }
        public int SeasonCount { get; set; }
        public string Overview { get; set; }
        public DateTime? FirstAirDate { get; set; }
        public DateTime? LastAirDate { get; set; }
        public List<Season> Seasons { get; set; }

        public List<Genre> Genres { get; set; }
        public bool InProduction { get; set; }
        public float Popularity { get; set; }
        public float VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int NumberOfEpisodes { get; set; }


        //public string Homepage { get; set; }
        //public int[] EpisodeRunTime { get; set; }
        //public string[] OriginCountry { get; set; }
        //public string OriginalLanguage { get; set; }
        //public string OriginalName { get; set; }
    }
}
