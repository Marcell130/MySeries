using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using MySeries.Api.Model;

namespace MySeries.Api.EF
{
	public class MySeriesDbContext : IdentityDbContext<ApplicationUser>
	{
		public MySeriesDbContext() : this( "MySeriesDbContext" )
		{
		}

		public MySeriesDbContext( string connectionString ) : base( connectionString, false )
		{
		    Database.CommandTimeout = 600;
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
		}
		
		public DbSet<Episode> Episodes { get; set; }
		public DbSet<Season> Seasons { get; set; }
		public DbSet<TvShow> TvShows { get; set; }
		public DbSet<UserTvShow> UserTvShows { get; set; }
		public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
		{
			base.OnModelCreating( modelBuilder );

			modelBuilder.Entity<ApplicationUser>().ToTable( "Identity.Users" );
			modelBuilder.Entity<IdentityRole>().ToTable( "Identity.Roles" );
			modelBuilder.Entity<IdentityUserRole>().ToTable( "Identity.UserRoles" );
			modelBuilder.Entity<IdentityUserClaim>().ToTable( "Identity.UserClaims" );
			modelBuilder.Entity<IdentityUserLogin>().ToTable( "Identity.UserLogins" );
		}

		public static MySeriesDbContext Create()
		{
			return new MySeriesDbContext();
		}
	}
}
