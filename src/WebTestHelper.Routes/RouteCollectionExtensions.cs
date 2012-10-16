using System.Web;
using System.Web.Routing;
using WebTestHelper.Fakes;

namespace WebTestHelper.Routes
{
    public static class RouteCollectionExtensions
    {
        public static RouteData Url(this RouteCollection routes, string url)
        {
            HttpContextBase httpContext = new FakeHttpContext(url);
            return routes.GetRouteData(httpContext);
        }
    }
}
