using LibraryManagement.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LibraryManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services


            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new CacheResultAttribute
            {
                Duration = 60
            });

            config.Filters.Add(new CustomExceptionAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
