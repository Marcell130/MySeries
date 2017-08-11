using System;
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
		private GenericRepository<TvShow> tvShowRepository;
		
		public AccountRepository AccountRepository
		{
			get
			{
				if( this.accountRepository == null )
				{
					this.accountRepository = new AccountRepository( context );
				}
				return accountRepository;
			}
		}

		public GenericRepository<TvShow> TvShowRepository
		{
			get
			{
				if( this.tvShowRepository == null )
				{
					this.tvShowRepository = new GenericRepository<TvShow>( context );
				}
				return tvShowRepository;
			}
		}

		public void Save()
		{
			context.SaveChanges();
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
