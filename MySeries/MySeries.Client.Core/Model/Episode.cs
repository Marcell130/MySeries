using System;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.Model
{
    public class Episode
    {
        public int Id { get; set; }

        public string EpisodeTitle { get; set; }
        public string EpisodeNumber { get; set; }
        public string Overview { get; set; }
        public DateTime? AirDate { get; set; }

        public string SeriesTitle { get; set; }

        public string SeriesPosterUriPostfix { private get; set; }
        public string EpisodeWallpaperUriPostfix { private get; set; }

        //public string SeriesPosterUriLarge => ThumbnailHelper.GetThumbnailUri(SeriesPosterUriPostfix, ThumbnailSize.Large);
        //public string SeriesPosterUriMedium => ThumbnailHelper.GetThumbnailUri(SeriesPosterUriPostfix, ThumbnailSize.Medium);
        public string SeriesPosterUriSmall => ThumbnailHelper.GetThumbnailUri(SeriesPosterUriPostfix, ThumbnailSize.Small);

        public string EpisodeWallpaperUriLarge => ThumbnailHelper.GetThumbnailUri(EpisodeWallpaperUriPostfix, ThumbnailSize.Large);
        public string EpisodeWallpaperUriMedium => ThumbnailHelper.GetThumbnailUri(EpisodeWallpaperUriPostfix, ThumbnailSize.Medium);
        public string EpisodeWallpaperUriSmall => ThumbnailHelper.GetThumbnailUri(EpisodeWallpaperUriPostfix, ThumbnailSize.Small);

    }
}
