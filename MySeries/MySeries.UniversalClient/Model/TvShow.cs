using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.Model
{
    public class TvShow
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string WallpaperUri { get; set; }
        public int SeasonCount { get; set; }
        public string Overview { get; set; }

        private string posterUriPostfix;
        public string PosterUri
        {
            set => this.posterUriPostfix = value;
        }

        public DateTime? LastAirDate { get; set; }

        private DateTime? firstAirDate;
        public DateTime? FirstAirDate
        {
            set => this.firstAirDate = value;
        }

        public TvShowState State { get; set; }

        public int? OwnRating { get; set; }

        public DateTime AddedDate { get; set; }

        public Uri PosterUriMedium => ThumbnailService.GetThumbnailUri( this.posterUriPostfix, ThumbnailSize.Medium );

        public string FirstAirYear => this.firstAirDate?.Year.ToString() ?? "";
    }
}
