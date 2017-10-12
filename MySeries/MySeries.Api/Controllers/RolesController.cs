using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySeries.Api.Dto;
using MySeries.Api.Identity.Model;

namespace MySeries.Api.Controllers
{
    [Authorize( Roles = "Admin" )]
    [RoutePrefix( "api/roles" )]
    public class RolesController : BaseApiController
    {

        [Route( "{id:guid}", Name = "GetRoleById" )]
        public async Task<IHttpActionResult> GetRole( string Id )
        {
            var role = await AppRoleManager.FindByIdAsync( Id );

            if( role != null )
            {
                return Ok(role.ToDto());
            }

            return NotFound();

        }

        [Route( "", Name = "GetAllRoles" )]
        public IHttpActionResult GetAllRoles()
        {
            var roles = AppRoleManager.Roles;

            return Ok( roles );
        }

        [Route( "create" )]
        public async Task<IHttpActionResult> Create( CreateRoleBindingModel model )
        {
            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var role = new IdentityRole { Name = model.Name };

            var result = await AppRoleManager.CreateAsync( role );

            if( !result.Succeeded )
            {
                return GetErrorResult( result );
            }

            Uri locationHeader = new Uri( Url.Link( "GetRoleById", new { id = role.Id } ) );

            return Created( locationHeader, role.ToDto() );
        }

        [Route( "{id:guid}" )]
        public async Task<IHttpActionResult> DeleteRole( string id )
        {
            var role = await AppRoleManager.FindByIdAsync( id );

            if( role != null )
            {
                IdentityResult result = await AppRoleManager.DeleteAsync( role );

                if( !result.Succeeded )
                {
                    return GetErrorResult( result );
                }

                return Ok();
            }

            return NotFound();

        }

        [Route( "ManageUsersInRole" )]
        public async Task<IHttpActionResult> ManageUsersInRole( UsersInRoleModel model )
        {
            var role = await AppRoleManager.FindByIdAsync( model.Id );

            if( role == null )
            {
                ModelState.AddModelError( "", "Role does not exist" );
                return BadRequest( ModelState );
            }

            foreach( string user in model.EnrolledUsers )
            {
                var appUser = await AppUserManager.FindByIdAsync( user );

                if( appUser == null )
                {
                    ModelState.AddModelError( "", $"User {user} does not exists");
                    continue;
                }

                if( !AppUserManager.IsInRole( user, role.Name ) )
                {
                    IdentityResult result = await AppUserManager.AddToRoleAsync( user, role.Name );

                    if( !result.Succeeded )
                    {
                        ModelState.AddModelError( "", $"User {user} could not be added to role");
                    }

                }
            }

            foreach( string user in model.RemovedUsers )
            {
                var appUser = await AppUserManager.FindByIdAsync( user );

                if( appUser == null )
                {
                    ModelState.AddModelError( "", $"User {user} does not exists");
                    continue;
                }

                IdentityResult result = await AppUserManager.RemoveFromRoleAsync( user, role.Name );

                if( !result.Succeeded )
                {
                    ModelState.AddModelError( "", $"User {user} could not be removed from role");
                }
            }

            if( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            return Ok();
        }
    }
}