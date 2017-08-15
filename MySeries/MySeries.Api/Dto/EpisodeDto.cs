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

        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime? AirDate { get; set; }
        public int EpisodeNumber { get; set; }
        public string PosterUri { get; set; }
    }

    public static class EpisodeExtension
    {
        public static EpisodeDto ToDto( this Episode episode )
        {
            var episodeDto = new EpisodeDto
            {
                Id = episode.Id,
                PosterUri = episode.PosterUri,
                Title = episode.Title,
                AirDate = episode.AirDate,
                EpisodeNumber = episode.EpisodeNumber,
                Overview = episode.Overview
            };
            return episodeDto;
        }
    }
}