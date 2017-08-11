using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using MySeries.Api.EF;
using MySeries.Api.Identity;
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

			ConfigureWebApi( config );

			app.UseCors( Microsoft.Owin.Cors.CorsOptions.AllowAll );
			
			app.UseWebApi( config );
			
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure( WebApiConfig.Register );
			FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
			RouteConfig.RegisterRoutes( RouteTable.Routes );
			BundleConfig.RegisterBundles( BundleTable.Bundles );
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
		}
		
		private void ConfigureWebApi( HttpConfiguration config )
		{
			config.MapHttpAttributeRoutes();

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}