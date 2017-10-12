using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySeries.DbCopyService.Model;

namespace MySeries.DbCopyService.Tmdb.Model
{
    public static class ModelExtensions
    {
        public static TvShow ToTvShow( this TvShowDetails details )
        {
            var tvShow = new TvShow
            {
                TmdbId = details.Id,
                Title = details.Name,
                Overview = details.Overview,
                WallpaperUriPostfix = details.BackdropPath,
                //EpisodeRunTime = details.EpisodeRunTime,
                FirstAirDate = String.IsNullOrEmpty( details.FirstAirDate ) ? (DateTime?)null : DateTime.ParseExact( details.FirstAirDate, "yyyy-MM-dd", CultureInfo.InvariantCulture ),
                Genres = details.Genres.Select( g => g.ToGenre() ).ToList(),
                //Homepage = details.Homepage,
                InProduction = details.InProduction,
                LastAirDate = String.IsNullOrEmpty( details.LastAirDate ) ? (DateTime?)null : DateTime.ParseExact( details.LastAirDate, "yyyy-MM-dd", CultureInfo.InvariantCulture ),
                NumberOfEpisodes = details.NumberOfEpisodes,
                SeasonCount = details.NumberOfSeasons,
                //OriginalName = details.OriginalName,
                Popularity = details.Popularity,
                PosterUriPostfix = details.PosterPath,
                Status = details.Status,
                Type = details.Type,
                VoteAverage = details.VoteAverage,
                VoteCount = details.VoteCount,
                Seasons = new List<Season>()
            };
            return tvShow;
        }

        public static Season ToSeason( this SeasonDetails details, int tvShowId )
        {
            var season = new Season
            {
                TmdbId = details.Id,
                TvShowId = tvShowId,
                Title = details.Name,
                Overview = details.Overview,
                PosterUriPostfix = details.PosterPath,
                EpisodeCount = details.Episodes.Length,
                SeasonNumber = details.SeasonNumber,
                AirDate = String.IsNullOrEmpty( details.AirDate ) ? (DateTime?)null : DateTime.ParseExact( details.AirDate, "yyyy-MM-dd", CultureInfo.InvariantCulture ),
                Episodes = details.Episodes.Select( e => e.ToEpisode( details.Id ) ).ToList(),
            };
            return season;
        }

        public static Episode ToEpisode( this SeasonEpisode details, int seasonId )
        {
            var episode = new Episode
            {
                TmdbId = details.Id,
                SeasonId = seasonId,
                Title = details.Name,
                AirDate = String.IsNullOrEmpty( details.AirDate ) ? (DateTime?)null : DateTime.ParseExact( details.AirDate, "yyyy-MM-dd", CultureInfo.InvariantCulture ),
                EpisodeNumber = details.EpisodeNumber,
                Overview = details.Overview,
                VoteCount = details.VoteCount,
                VoteAverage = details.VoteAverage,
                PosterUriPostfix = details.StillPath,
            };
            return episode;
        }

        public static Genre ToGenre( this TmdbGenre tg )
        {
            var genre = new Genre
            {
                Name = tg.Name,
                TmdbId = tg.Id
            };
            return genre;
        }
    }
}
