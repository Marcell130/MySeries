using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MySeries.Api.Model
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.None )]
        public int TmdbId { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public List<TvShow> TvShows { get; set; }
    }
}
