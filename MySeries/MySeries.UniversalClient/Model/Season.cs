using System;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.Model
{
    public class Season
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }

        private string posterUriPostfix;
        public string PosterUri
        {
            set => this.posterUriPostfix = value;
        }
        public Uri PosterUriMedium => ThumbnailService.GetThumbnailUri( this.posterUriPostfix, ThumbnailSize.Medium );

        public string Title => $"Season {SeasonNumber}";
        public string SubTitle => EpisodeCount == 0 ? "Specials" : $"{EpisodeCount} Episodes";

    }
}
