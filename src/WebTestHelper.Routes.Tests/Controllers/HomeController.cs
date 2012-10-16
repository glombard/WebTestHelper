using System.Web.Mvc;

namespace WebTestHelper.Routes.Tests.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
