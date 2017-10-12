using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.Model;

namespace MySeries.Api.Dto
{
    public class EpisodeDto
    {
        public int Id { get; set; }

        public int EpisodeNumber { get; set; }
        public string EpisodeTitle { get; set; }
        public DateTime? AirDate { get; set; }
    }

    public class EpisodeDetailedDto
    {
        public int Id { get; set; }

        public string EpisodeTitle { get; set; }
        public string Overview { get; set; }
        public DateTime? AirDate { get; set; }

        public int EpisodeNumber { get; set; }

        public string SeriesTitle { get; set; }

        public string SeriesPosterUriPostfix { get; set; }
        public string EpisodeWallpaperUriPostfix { set; get; }
    }

    public static class EpisodeExtension
    {
        public static EpisodeDto ToDto( this Episode episode )
        {
            var episodeDto = new EpisodeDto
            {
                Id = episode.TmdbId,
                EpisodeTitle = episode.Title,
                EpisodeNumber = episode.EpisodeNumber,
                AirDate = episode.AirDate,
            };
            return episodeDto;
        }

        public static EpisodeDetailedDto ToDetailedDto( this Episode episode )
        {
            var episodeDto = new EpisodeDetailedDto
            {
                Id = episode.TmdbId,
                EpisodeWallpaperUriPostfix = episode.PosterUriPostfix,
                SeriesPosterUriPostfix = episode.Season?.TvShow?.PosterUriPostfix,
                EpisodeTitle = episode.Title,
                EpisodeNumber = episode.EpisodeNumber,
                SeriesTitle = episode.Season?.TvShow?.Title,
                AirDate = episode.AirDate,
                Overview = episode.Overview
            };
            return episodeDto;
        }
    }
}