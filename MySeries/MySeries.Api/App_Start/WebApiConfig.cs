using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using MySeries.Api.GlobalHandlers;
using MySeries.Api.GlobalHandlers.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MySeries.Api
{
    public static class WebApiConfig
    {
        public static void Register( HttpConfiguration config )
        {
            config.Services.Replace( typeof( IExceptionHandler ), new ApplicationExceptionHandler() );
            config.Services.Add( typeof( IExceptionLogger ), new SurveyAdminExceptionLogger() );
            config.MessageHandlers.Add( new RequestLogger() );

            //TODO
            //GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.S‌​erializerSettings.Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.S‌​erializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Filters.Add( new ValidationActionFilter() );

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
