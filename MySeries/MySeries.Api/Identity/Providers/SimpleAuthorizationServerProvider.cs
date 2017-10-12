using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using MySeries.Api.EF;
using MySeries.Api.Identity.Managers;
using MySeries.Api.Model;
using MySeries.Api.Repositories;

namespace MySeries.Api.Identity.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new MySeriesDbContext()));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(new MySeriesDbContext()));

            var user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(new GenericIdentity(user.UserName, "TokenAuth"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            //identity.AddClaim( new Claim( "sub", context.UserName ) );
            //identity.AddClaim( new Claim( "role", "user" ) );

            var roles = roleManager.Roles.Where(r => r.Users.Any(u => u.UserId == user.Id)).ToList();
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            context.Validated(identity);

        }
    }
}