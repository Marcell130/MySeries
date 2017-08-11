﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.Model;

namespace MySeries.Api.Dto
{
	public class TvShowDto
	{
		public int TmdbId { get; set; }

		public string Title { get; set; }
		public string WallpaperUri { get; set; }
		public string PosterUri { get; set; }
		public int SeasonCount { get; set; }
		public string Overview { get; set; }

		public DateTime? LastAirDate { get; set; }
		public DateTime? FirstAirDate { get; set; }

		public TvShowState State { get; set; }

		public int? OwnRating { get; set; }
		public DateTime AddedDate { get; set; }
	}

	public static class TvShowExtension
	{
		public static TvShowDto ToDto( this TvShow tvShow, DateTime addedDate, int? ownRating )
		{
			return new TvShowDto
			{
				TmdbId = tvShow.TmdbId,
				Title = tvShow.Title,
				WallpaperUri = tvShow.WallpaperUri,
				PosterUri = tvShow.PosterUri,
				SeasonCount = tvShow.SeasonCount,
				Overview = tvShow.Overview,
				LastAirDate = tvShow.LastAirDate,
				FirstAirDate = tvShow.FirstAirDate,
				State = tvShow.State,
				OwnRating = ownRating,
				AddedDate = addedDate
			};
		}
	}
}