using System.Collections.Generic;

namespace MySeries.Api.Model
{
	public class User
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public List<TvShow> TvShows { get; set; }
	}
}
