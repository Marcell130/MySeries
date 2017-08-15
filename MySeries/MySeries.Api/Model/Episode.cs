﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySeries.Api.Model
{
    public class Episode
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }

        public DateTime? AirDate { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterUri { get; set; }

        [ForeignKey( "SeasonId" )]
        public Season Season { get; set; }
        public int SeasonId { get; set; }
    }
}
