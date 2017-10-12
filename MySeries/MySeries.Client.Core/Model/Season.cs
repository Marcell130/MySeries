using System.Collections.Generic;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Core.Model
{
    public class Season
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }

        public string PosterUriPostfix { private get; set; }
        public string PosterUriSmall => ThumbnailHelper.GetThumbnailUri( PosterUriPostfix, ThumbnailSize.Small );

        public string Title { get; set; }

        public List<Episode> Episodes { get; set; }
    }
}
