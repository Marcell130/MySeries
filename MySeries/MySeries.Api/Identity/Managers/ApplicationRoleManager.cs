using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MySeries.Api.EF;

namespace MySeries.Api.Identity.Managers
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager( IRoleStore<IdentityRole, string> roleStore ) : base( roleStore )
        {
        }

        public static ApplicationRoleManager Create( IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context )
        {
            var appRoleManager = new ApplicationRoleManager( new RoleStore<IdentityRole>( context.Get<MySeriesDbContext>() ) );

            return appRoleManager;
        }

    }
}