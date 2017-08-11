using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MySeries.Api.Model;

namespace MySeries.Api.Identity.Validators
{
	public class MyCustomUserValidator : UserValidator<ApplicationUser>
	{
		private readonly List<string> allowedEmailDomains = new List<string> { "outlook.com", "hotmail.com", "gmail.com", "yahoo.com" };

		public MyCustomUserValidator( ApplicationUserManager appUserManager ): base( appUserManager )
		{
		}

		public override async Task<IdentityResult> ValidateAsync( ApplicationUser user )
		{
			var result = await base.ValidateAsync( user );

			var emailDomain = user.Email.Split( '@' )[1];

			if( !allowedEmailDomains.Contains( emailDomain.ToLower() ) )
			{
				var errors = result.Errors.ToList();

				errors.Add($"Email domain '{emailDomain}' is not allowed");

				result = new IdentityResult( errors );
			}

			return result;
		}
	}
}