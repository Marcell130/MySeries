using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.Model;

namespace MySeries.Api.Dto
{
    public class TvShowDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string PosterUriPostfix { get; set; }

        public DateTime? FirstAirDate { get; set; }

        public TvShowState State { get; set; }
    }
    public class TvShowDetailedDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string WallpaperUriPostfix { get; set; }
        public string PosterUriPostfix { get; set; }
        public int SeasonCount { get; set; }
        public string Overview { get; set; }

        public DateTime? LastAirDate { get; set; }
        public DateTime? FirstAirDate { get; set; }

        public TvShowState State { get; set; }

        public int? OwnRating { get; set; }
        public DateTime AddedDate { get; set; }

        public List<SeasonDto> Seasons { get; set; }
    }

    public static class TvShowExtension
    {
        public static TvShowDto ToDto( this TvShow tvShow )
        {
            var tvShowDto = new TvShowDto
            {
                Id = tvShow.TmdbId,
                Title = tvShow.Title,
                PosterUriPostfix = tvShow.PosterUriPostfix,
                FirstAirDate = tvShow.FirstAirDate,
                State = tvShow.State,
            };

            return tvShowDto;
        }

        public static TvShowDetailedDto ToDetailedDto( this TvShow tvShow )
        {
            var tvShowDto = new TvShowDetailedDto
            {
                Id = tvShow.TmdbId,
                Title = tvShow.Title,
                WallpaperUriPostfix = tvShow.WallpaperUriPostfix,
                PosterUriPostfix = tvShow.PosterUriPostfix,
                SeasonCount = tvShow.SeasonCount,
                Overview = tvShow.Overview,
                LastAirDate = tvShow.LastAirDate,
                FirstAirDate = tvShow.FirstAirDate,
                State = tvShow.State,
                Seasons = tvShow.Seasons?.Select( s => s.ToDto() ).ToList(),
            };

            return tvShowDto;
        }

        public static TvShowDetailedDto ToDetailedDto( this TvShow tvShow, DateTime addedDate, int? ownRating )
        {
            var tvShowDto = new TvShowDetailedDto
            {
                Id = tvShow.TmdbId,
                Title = tvShow.Title,
                WallpaperUriPostfix = tvShow.WallpaperUriPostfix,
                PosterUriPostfix = tvShow.PosterUriPostfix,
                SeasonCount = tvShow.SeasonCount,
                Overview = tvShow.Overview,
                LastAirDate = tvShow.LastAirDate,
                FirstAirDate = tvShow.FirstAirDate,
                State = tvShow.State,
                Seasons = tvShow.Seasons?.Select( s => s.ToDto() ).ToList(),
                OwnRating = ownRating,
                AddedDate = addedDate,
                
            };

            return tvShowDto;
        }
    }
}