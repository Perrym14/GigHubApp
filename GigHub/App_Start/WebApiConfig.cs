using System.Web.Http;

namespace GigHub
{

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //routes.MapHttpRoute("RestApiRoute", "Api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" }); //this replaces your current api route


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
