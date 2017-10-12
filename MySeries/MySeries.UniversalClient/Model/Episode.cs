using System;
using Windows.UI;
using Windows.UI.Xaml.Media;
using MySeries.UniversalClient.Services;

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

        public Color? CustomColor { get; set; }

        public Color Color => CustomColor ?? ColorService.GetRandomColor( Title.GetHashCode() );

        public string EpisodeNumberString => $"Episode {EpisodeNumber}";
    }
}
