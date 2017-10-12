using System.Runtime.Serialization;
using MySeries.DbCopyService.Model;

namespace MySeries.DbCopyService.Tmdb.Model
{
    [DataContract]
    public class TvShowDetails
    {
        [DataMember( Name = "backdrop_path" )]
        public string BackdropPath { get; set; }

        [DataMember( Name = "episode_run_time" )]
        public int[] EpisodeRunTime { get; set; }

        [DataMember( Name = "first_air_date" )]
        public string FirstAirDate { get; set; }

        [DataMember( Name = "genres" )]
        public TmdbGenre[] Genres { get; set; }

        [DataMember( Name = "homepage" )]
        public string Homepage { get; set; }

        [DataMember( Name = "id" )]
        public int Id { get; set; }

        [DataMember( Name = "in_production" )]
        public bool InProduction { get; set; }

        //[DataMember( Name = "languages" )]
        //public string[] Languages { get; set; }

        [DataMember( Name = "last_air_date" )]
        public string LastAirDate { get; set; }

        [DataMember( Name = "name" )]
        public string Name { get; set; }

        [DataMember( Name = "number_of_episodes" )]
        public int NumberOfEpisodes { get; set; }

        [DataMember( Name = "number_of_seasons" )]
        public int NumberOfSeasons { get; set; }

        //[DataMember( Name = "origin_country" )]
        //public string[] OriginCountry { get; set; }

        [DataMember( Name = "original_language" )]
        public string OriginalLanguage { get; set; }

        [DataMember( Name = "original_name" )]
        public string OriginalName { get; set; }

        [DataMember( Name = "overview" )]
        public string Overview { get; set; }

        [DataMember( Name = "popularity" )]
        public float Popularity { get; set; }

        [DataMember( Name = "poster_path" )]
        public string PosterPath { get; set; }

        [DataMember( Name = "status" )]
        public string Status { get; set; }

        [DataMember( Name = "type" )]
        public string Type { get; set; }

        [DataMember( Name = "vote_average" )]
        public float VoteAverage { get; set; }

        [DataMember( Name = "vote_count" )]
        public int VoteCount { get; set; }

        [DataMember( Name = "seasons" )]
        public TvShowSeason[] Seasons { get; set; }
    }

    [DataContract]
    public class TvShowSeason
    {
        //[DataMember( Name = "id" )]
        //public int Id { get; set; }

        //[DataMember( Name = "air_date" )]
        //public string AirDate { get; set; }

        //[DataMember( Name = "episode_count" )]
        //public int EpisodeCount { get; set; }

        //[DataMember( Name = "poster_path" )]
        //public string PosterPath { get; set; }

        [DataMember( Name = "season_number" )]
        public int SeasonNumber { get; set; }
    }

}
