using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MySeries.Api.Identity;
using MySeries.Api.Model;
using MySeries.Api.Repositories;

namespace MySeries.Api.EF
{
    public class UnitOfWork : IDisposable
    {
        private readonly MySeriesDbContext context = new MySeriesDbContext();

        private AccountRepository accountRepository;
        private SeasonRepository seasonRepository;
        private TvShowRepository tvShowRepository;
        private EpisodeRepository episodeRepository;
        private GenreRepository genreRepository;

        public AccountRepository AccountRepository => this.accountRepository ?? (this.accountRepository = new AccountRepository( this.context ));
        public SeasonRepository SeasonRepository => this.seasonRepository ?? (this.seasonRepository = new SeasonRepository( this.context ));
        public TvShowRepository TvShowRepository => this.tvShowRepository ?? (this.tvShowRepository = new TvShowRepository( this.context ));
        public EpisodeRepository EpisodeRepository => this.episodeRepository ?? (this.episodeRepository = new EpisodeRepository( this.context ));
        public GenreRepository GenreRepository => this.genreRepository ?? (this.genreRepository = new GenreRepository( this.context ));


        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }

        private bool disposed;

        protected virtual void Dispose( bool disposing )
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }
    }
}
