using System.Runtime.Serialization;

namespace MySeries.DbCopyService.Tmdb.Model
{
    [DataContract]
    public class TmdbGenre
    {
        [DataMember( Name = "id" )]
        public int Id { get; set; }

        [DataMember( Name = "name" )]
        public string Name { get; set; }
    }
}
