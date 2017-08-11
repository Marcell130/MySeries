using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MySeries.Api.EF;
using MySeries.Api.Identity.Validators;
using MySeries.Api.Model;
using MySeries.Api.Services;

namespace MySeries.Api.Identity
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager( IUserStore<ApplicationUser> store ) : base( store )
		{
		}

		public static ApplicationUserManager Create( IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context )
		{
			var appDbContext = context.Get<MySeriesDbContext>();
			var appUserManager = new ApplicationUserManager( new UserStore<ApplicationUser>( appDbContext ) );

			appUserManager.EmailService = new EmailService();

			var dataProtectionProvider = options.DataProtectionProvider;
			if( dataProtectionProvider != null )
			{
				appUserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>( dataProtectionProvider.Create( "ASP.NET Identity" ) )
				{
					//Code for email confirmation and reset password life time
					TokenLifespan = TimeSpan.FromHours( 6 )
				};
			}

			appUserManager.UserValidator = new MyCustomUserValidator( appUserManager )
			{
				AllowOnlyAlphanumericUserNames = true,
				RequireUniqueEmail = true
			};

			appUserManager.PasswordValidator = new MyCustomPasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = false,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			return appUserManager;
		}
	}
}