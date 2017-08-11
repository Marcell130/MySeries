using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace MySeries.Api.Identity.Validators
{
	public class MyCustomPasswordValidator : PasswordValidator
	{
		public override async Task<IdentityResult> ValidateAsync( string password )
		{
			IdentityResult result = await base.ValidateAsync( password );

			if( password.Contains( "abcde" ) || password.Contains( "12345" ) )
			{
				var errors = result.Errors.ToList();
				errors.Add( "Password can not contain sequence of chars" );
				result = new IdentityResult( errors );
			}
			return result;
		}
	}
}