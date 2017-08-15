using System;

namespace MySeries.UniversalClient.Model
{
    public class Episode
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime? AirDate { get; set; }
        public int EpisodeNumber { get; set; }
        public string PosterUri { get; set; }

        public string EpisodeNumberString => $"Episode {EpisodeNumber}";
    }
}
