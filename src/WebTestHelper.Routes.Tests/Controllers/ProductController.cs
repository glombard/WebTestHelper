using System.Web.Mvc;

namespace WebTestHelper.Routes.Tests.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string category, int? page)
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
