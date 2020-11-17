using System.Web.Mvc;

namespace Flight_Planner_CurrectWay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
