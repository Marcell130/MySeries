using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySeries.Api.Dto;
using MySeries.Api.EF;
using MySeries.Api.Identity;
using MySeries.Api.Identity.Model;
using MySeries.Api.Model;

namespace MySeries.Api.Controllers
{
	[Authorize]
	[RoutePrefix( "api/accounts" )]
	public class AccountsController : ApiController
	{
		private readonly ApplicationUserManager _appUserManager = null;

		private ApplicationUserManager UserManager => _appUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

		private readonly UnitOfWork unitOfWork = new UnitOfWork();
		

		[Route( "users" )]
		public async Task<IHttpActionResult> GetUsers()
		{
			var users = (await this.unitOfWork.AccountRepository.GetUsers()).Select( u => u.ToDto() ).ToList();
			return Ok( users );
		}

		[AllowAnonymous]
		[Route( "create" )]
		public async Task<IHttpActionResult> CreateUser( CreateUserBindingModel userModel )
		{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var user = new ApplicationUser
				{
					UserName = userModel.Email,
					Email = userModel.Email,
					FirstName = userModel.FirstName,
					LastName = userModel.LastName,
					BirthDate = userModel.BirthDate.Value,
					Gender = userModel.Gender.Value,
					JoinDate = DateTime.Now.Date,
				};

			try
			{
				var addUserResult = await UserManager.CreateAsync(user, userModel.Password);

				if (!addUserResult.Succeeded)
				{
					return GetErrorResult(addUserResult);
				}

				//var user = await unitOfWork.AccountRepository.FindByNameAsync( createUserModel.Email );

				string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

				var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new {userId = user.Id, code = code}));

				await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

				return Created(new Uri(Url.Link("GetUserById", new {id = user.Id})), user.ToDto());
			}
			catch(Exception ex )
			{
				await UserManager.DeleteAsync(user);
				//TODO: throw new publicException could not create user
				return InternalServerError();
			}
		}

		[Route( "user/{id:guid}", Name = "GetUserById" )]
		public async Task<IHttpActionResult> GetUser( string Id )
		{
			var user = await UserManager.FindByIdAsync( Id );

			if( user != null )
			{
				return Ok( user.ToDto() );
			}

			return NotFound();
		}

		[Route( "user/{username}" )]
		public async Task<IHttpActionResult> GetUserByName( string username )
		{
			var user = await UserManager.FindByNameAsync( username );

			if( user != null )
			{
				return Ok( user.ToDto() );
			}

			return NotFound();
		}

		[HttpGet]
		[AllowAnonymous]
		[Route( "ConfirmEmail", Name = "ConfirmEmailRoute" )]
		public async Task<IHttpActionResult> ConfirmEmail( string userId = "", string code = "" )
		{
			if( string.IsNullOrWhiteSpace( userId ) || string.IsNullOrWhiteSpace( code ) )
			{
				ModelState.AddModelError( "", "User Id and Code are required" );
				return BadRequest( ModelState );
			}

			IdentityResult result = await UserManager.ConfirmEmailAsync( userId, code );

			if( result.Succeeded )
			{
				return Ok();
			}
			else
			{
				return GetErrorResult( result );
			}
		}

		[Route( "ChangePassword" )]
		public async Task<IHttpActionResult> ChangePassword( ChangePasswordBindingModel model )
		{
			if( !ModelState.IsValid )
			{
				return BadRequest( ModelState );
			}

			IdentityResult result = await UserManager.ChangePasswordAsync( User.Identity.GetUserId(), model.OldPassword, model.NewPassword );

			if( !result.Succeeded )
			{
				return GetErrorResult( result );
			}

			return Ok();
		}

		private IHttpActionResult GetErrorResult( IdentityResult result )
		{
			if( result == null )
			{
				return InternalServerError();
			}

			if( !result.Succeeded )
			{
				if( result.Errors != null )
				{
					foreach( string error in result.Errors )
					{
						ModelState.AddModelError( "", error );
					}
				}

				if( ModelState.IsValid )
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest( ModelState );
			}

			return null;
		}
	}
}