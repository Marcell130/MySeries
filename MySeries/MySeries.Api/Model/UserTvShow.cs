using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySeries.Api.Model
{
	public class UserTvShow
	{
		public int Id { get; set; }

		[ForeignKey( "UserId" )]
		public ApplicationUser User { get; set; }

		public string UserId { get; set; }


		[ForeignKey( "TvShowId" )]
		public TvShow TvShow { get; set; }
		public int TvShowId { get; set; }

		public int? Rating { get; set; }

	    [Column( TypeName = "Date" )]
        public DateTime AddedDate { get; set; }
	}
}