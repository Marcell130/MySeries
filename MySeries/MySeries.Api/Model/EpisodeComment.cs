using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MySeries.Api.Model
{
    public class EpisodeComment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey( "EpisodeId" )]
        public Episode Episode { get; set; }
        public int EpisodeId { get; set; }

        [ForeignKey( "UserId" )]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [Column( TypeName = "Date" )]
        public DateTime Timestamp { get; set; }

        [StringLength( 500 )]
        public string Text { get; set; }
    }
}