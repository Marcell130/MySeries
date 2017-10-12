using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySeries.Api.Dto;
using MySeries.Api.EF;
using MySeries.Api.GlobalHandlers.Exceptions;
using MySeries.Api.Identity.Managers;
using MySeries.Api.Identity.Model;
using MySeries.Api.Model;

namespace MySeries.Api.Controllers
{
	[Authorize(Roles="Admin")]
	[RoutePrefix( "api/Accounts" )]
	public class AccountsController : BaseApiController
	{
		private readonly UnitOfWork unitOfWork = new UnitOfWork();
		

		[Route( "Users" )]
		public async Task<IHttpActionResult> GetUsers()
		{
			var users = (await this.unitOfWork.AccountRepository.GetUsers()).Select( u => u.ToDto() ).ToList();
			return Ok( users );
		}

		[AllowAnonymous]
		[Route( "Create" )]
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
				var addUserResult = await AppUserManager.CreateAsync(user, userModel.Password);

				if (!addUserResult.Succeeded)
				{
					return GetErrorResult(addUserResult);
				}

				//var user = await unitOfWork.AccountRepository.FindByNameAsync( createUserModel.Email );

				string code = await AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

				var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new {userId = user.Id, code = code}));

				await AppUserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

				return Created(new Uri(Url.Link("GetUserById", new {id = user.Id})), user.ToDto());
			}
			catch(Exception ex )
			{
				await AppUserManager.DeleteAsync(user);

				throw new BusinessLogicException($"Could not create user: {ex.Message}");
			}
		}

		[Route( "User/{id:guid}", Name = "GetUserById" )]
		public async Task<IHttpActionResult> GetUser( string Id )
		{
			var user = await AppUserManager.FindByIdAsync( Id );

			if( user != null )
			{
				return Ok( user.ToDto() );
			}

			return NotFound();
		}

		[Route( "User/{username}" )]
		public async Task<IHttpActionResult> GetUserByName( string username )
		{
			var user = await AppUserManager.FindByNameAsync( username );

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

			IdentityResult result = await AppUserManager.ConfirmEmailAsync( userId, code );

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

			IdentityResult result = await AppUserManager.ChangePasswordAsync( User.Identity.GetUserId(), model.OldPassword, model.NewPassword );

			if( !result.Succeeded )
			{
				return GetErrorResult( result );
			}

			return Ok();
		}

	    [Authorize( Roles = "Admin" )]
	    [Route( "user/{id:guid}/roles" )]
	    [HttpPut]
	    public async Task<IHttpActionResult> AssignRolesToUser( [FromUri] string id, [FromBody] string[] rolesToAssign )
	    {

	        var appUser = await AppUserManager.FindByIdAsync( id );

	        if( appUser == null )
	        {
	            return NotFound();
	        }

	        var currentRoles = await AppUserManager.GetRolesAsync( appUser.Id );

	        var rolesNotExists = rolesToAssign.Except( AppRoleManager.Roles.Select( x => x.Name ) ).ToArray();

	        if( rolesNotExists.Any() )
	        {

	            ModelState.AddModelError( "", $"Roles '{string.Join(",", rolesNotExists)}' does not exixts in the system");
	            return BadRequest( ModelState );
	        }

	        IdentityResult removeResult = await AppUserManager.RemoveFromRolesAsync( appUser.Id, currentRoles.ToArray() );

	        if( !removeResult.Succeeded )
	        {
	            ModelState.AddModelError( "", "Failed to remove user roles" );
	            return BadRequest( ModelState );
	        }

	        IdentityResult addResult = await AppUserManager.AddToRolesAsync( appUser.Id, rolesToAssign );

	        if( !addResult.Succeeded )
	        {
	            ModelState.AddModelError( "", "Failed to add user roles" );
	            return BadRequest( ModelState );
	        }

	        return Ok();
	    }
    }
}