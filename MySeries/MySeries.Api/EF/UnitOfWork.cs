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

        public AccountRepository AccountRepository => accountRepository ?? (this.accountRepository = new AccountRepository( this.context ));
        public SeasonRepository SeasonRepository => seasonRepository ?? (this.seasonRepository = new SeasonRepository( this.context ));


        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed;

        protected virtual void Dispose( bool disposing )
        {
            if( !this.disposed )
            {
                if( disposing )
                {
                    context.Dispose();
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
