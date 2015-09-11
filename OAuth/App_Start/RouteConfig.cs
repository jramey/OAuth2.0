using System.Web.Mvc;
using System.Web.Routing;

namespace OAuth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "OAuth", url: "Login/OAuth/{action}", defaults: new { controller = "Login" });
            routes.MapRoute(name: "User", url: "User", defaults: new { controller = "Users", action = "GetUserInformation" });
        }
    }
}
