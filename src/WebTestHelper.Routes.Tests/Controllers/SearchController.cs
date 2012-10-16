using System.Web.Mvc;

namespace WebTestHelper.Routes.Tests.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Result(string query, int page)
        {
            return View();
        }
    }
}
