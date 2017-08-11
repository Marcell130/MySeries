using System.Collections.Generic;

namespace MySeries.Api.Model
{
	public class Season
	{
		public int Id { get; set; }
		public int TmdbId { get; set; }
		public int SeasonNumber { get; set; }
		public int EpisodeCount { get; set; }
		public List<Episode> Episodes { get; set; }

		public TvShow TvShow { get; set; }
	}
}
