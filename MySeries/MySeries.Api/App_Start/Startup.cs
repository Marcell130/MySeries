using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MySeries.Api.EF;
using MySeries.Api.Identity.Managers;
using MySeries.Api.Identity.Providers;
using Newtonsoft.Json.Serialization;
using Owin;

namespace MySeries.Api
{
	public class Startup
	{

		public void Configuration( IAppBuilder app )
		{
			var config = new HttpConfiguration();

			ConfigureOAuth( app );
			
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure( WebApiConfig.Register );
			FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
			RouteConfig.RegisterRoutes( RouteTable.Routes );
			BundleConfig.RegisterBundles( BundleTable.Bundles );
			
			app.UseWebApi( config );
		}

		public void ConfigureOAuth( IAppBuilder app )
		{
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString( "/token" ),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays( 1 ),
				Provider = new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer( OAuthServerOptions );
			app.UseOAuthBearerAuthentication( new OAuthBearerAuthenticationOptions() );

            app.CreatePerOwinContext( MySeriesDbContext.Create );
			app.CreatePerOwinContext<ApplicationUserManager>( ApplicationUserManager.Create );

		    app.UseCors( Microsoft.Owin.Cors.CorsOptions.AllowAll );
        }
	}
}