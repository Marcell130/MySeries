using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySeries.Api.Identity.Managers;

namespace MySeries.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        //Code removed from brevity
        private readonly ApplicationUserManager _appUserManager = null;
        private readonly ApplicationRoleManager _appRoleManager = null;

        protected ApplicationUserManager AppUserManager => this._appUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        protected ApplicationRoleManager AppRoleManager => this._appRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();




        internal IHttpActionResult GetErrorResult( IdentityResult result )
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