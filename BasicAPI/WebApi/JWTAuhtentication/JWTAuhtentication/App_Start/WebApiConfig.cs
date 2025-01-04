using JWTAuhtentication.Helpers;
using Swashbuckle.Application;
using System.Web.Http;

namespace JWTAuhtentication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new CacheResultAttribute { Duration = 60 });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
