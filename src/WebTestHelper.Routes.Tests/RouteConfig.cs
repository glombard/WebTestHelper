using System.Web.Mvc;
using System.Web.Routing;

namespace WebTestHelper.Routes.Tests
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NoController",
                url: "NoController/{action}",
                defaults: new { action = "Index" }
            );

            routes.MapRoute(
                name: "HomeNoAction",
                url: "HomeNoAction",
                defaults: new { controller = "Home" }
            );

            routes.MapRoute(
                name: "SearchMissingParam",
                url: "SearchMissingParam/{query}",
                defaults: new { controller = "Search", action = "Result" }
            );

            routes.MapRoute(
                name: "Search",
                url: "search/{query}/{page}",
                defaults: new { controller = "Search", action = "Result", page = 1 }
            );

            routes.MapRoute(
                name: "ProductList",
                url: "product/list/{category}/{page}",
                defaults: new { controller = "Product", action = "List", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
