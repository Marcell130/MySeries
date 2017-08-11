using System.ComponentModel.DataAnnotations.Schema;

namespace MySeries.Api.Model
{
	public class UserTvShow
	{
		public int Id { get; set; }

		[ForeignKey( "UserId" )]
		public User User { get; set; }

		public string UserId { get; set; }


		[ForeignKey( "TvShowId" )]
		public TvShow TvShow { get; set; }
		public int TvShowId { get; set; }
	}
}