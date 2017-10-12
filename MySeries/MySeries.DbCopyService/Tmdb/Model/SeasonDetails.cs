using System.Runtime.Serialization;

namespace MySeries.DbCopyService.Tmdb.Model
{
    [DataContract]
    public class SeasonDetails
    {
        [DataMember( Name = "id" )]
        public int Id { get; set; }

        [DataMember( Name = "air_date" )]
        public string AirDate { get; set; }

        [DataMember( Name = "name" )]
        public string Name { get; set; }

        [DataMember( Name = "overview" )]
        public string Overview { get; set; }

        [DataMember( Name = "poster_path" )]
        public string PosterPath { get; set; }

        [DataMember( Name = "season_number" )]
        public int SeasonNumber { get; set; }

        [DataMember( Name = "episodes" )]
        public SeasonEpisode[] Episodes { get; set; }
    }

    [DataContract]
    public class SeasonEpisode
    {
        [DataMember( Name = "air_date" )]
        public string AirDate { get; set; }

        [DataMember( Name = "episode_number" )]
        public int EpisodeNumber { get; set; }

        [DataMember( Name = "name" )]
        public string Name { get; set; }

        [DataMember( Name = "overview" )]
        public string Overview { get; set; }

        [DataMember( Name = "id" )]
        public int Id { get; set; }

        [DataMember( Name = "production_code" )]
        public string ProductionCode { get; set; }

        [DataMember( Name = "season_number" )]
        public int SeasonNumber { get; set; }

        [DataMember( Name = "still_path" )]
        public string StillPath { get; set; }

        [DataMember( Name = "vote_average" )]
        public float VoteAverage { get; set; }

        [DataMember( Name = "vote_count" )]
        public int VoteCount { get; set; }

        //public Crew[] crew { get; set; }
        //public Guest_Stars[] guest_stars { get; set; }
    }

    //public class Crew
    //{
    //    public string credit_id { get; set; }
    //    public string department { get; set; }
    //    public int? gender { get; set; }
    //    public int id { get; set; }
    //    public string job { get; set; }
    //    public string name { get; set; }
    //    public string profile_path { get; set; }
    //}

    //public class Guest_Stars
    //{
    //    public string character { get; set; }
    //    public string credit_id { get; set; }
    //    public int? gender { get; set; }
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public int order { get; set; }
    //    public string profile_path { get; set; }
    //}

}
