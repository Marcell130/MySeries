using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySeries.Api.EF;
using MySeries.Api.Model;
using System.Data.Entity;
using System.Threading.Tasks;
using MySeries.Api.Dto;


namespace MySeries.Api.Repositories
{
    public class GenreRepository
    {
        private readonly MySeriesDbContext context;

        public GenreRepository( MySeriesDbContext context )
        {
            this.context = context;
        }
        
        public void InsertOrUpdate( Genre entity )
        {
            if (this.context.Genres.Any( e => e.TmdbId == entity.TmdbId ))
            {
                this.context.Genres.Attach( entity );
                this.context.Entry( entity ).State = EntityState.Modified;
            }
            else
            {
                this.context.Genres.Add( entity );
            }
        }
    }
}