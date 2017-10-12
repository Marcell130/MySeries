using System;
using System.Collections.Generic;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.Model
{
    public class TvShow
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int SeasonCount { get; set; }
        public string Overview { get; set; }


        public DateTime? LastAirDate { get; set; }

        private DateTime? firstAirDate;

        public DateTime? FirstAirDate
        {
            set => this.firstAirDate = value;
        }

        public TvShowState State { get; set; }

        public int? OwnRating { get; set; }

        public DateTime AddedDate { get; set; }

        public string PosterUriPostfix { set; private get; }
        public string WallpaperUriPostfix { set; private get; }

        public string PosterUriLarge => ThumbnailHelper.GetThumbnailUri(PosterUriPostfix, ThumbnailSize.Large);
        public string PosterUriMedium => ThumbnailHelper.GetThumbnailUri(PosterUriPostfix, ThumbnailSize.Medium);
        public string PosterUriSmall => ThumbnailHelper.GetThumbnailUri(PosterUriPostfix, ThumbnailSize.Small);

        public string WallpaperUriLarge => ThumbnailHelper.GetThumbnailUri(WallpaperUriPostfix, ThumbnailSize.Large);
        public string WallpaperUriMedium => ThumbnailHelper.GetThumbnailUri(WallpaperUriPostfix, ThumbnailSize.Medium);
        public string WallpaperUriSmall => ThumbnailHelper.GetThumbnailUri(WallpaperUriPostfix, ThumbnailSize.Small);

        public string FirstAirYear => this.firstAirDate?.Year.ToString() ?? "";

        public List<Season> Seasons { get; set; }
    }
}
