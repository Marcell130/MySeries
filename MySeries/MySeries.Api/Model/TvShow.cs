using System;
using System.Collections.Generic;

namespace MySeries.Api.Model
{
	public class TvShow
	{
		public int Id { get; set; }
		public int TmdbId { get; set; }

		public string Title { get; set; }
		public string WallpaperUri { get; set; }
		public string PosterUri { get; set; }
		public int SeasonCount { get; set; }
		public string Overview { get; set; }

		public DateTime? LastAirDate { get; set; }
		public DateTime? FirstAirDate { get; set; }
		public virtual List<Season> Seasons { get; set; }

		public TvShowState State { get; set; }
	}
}
