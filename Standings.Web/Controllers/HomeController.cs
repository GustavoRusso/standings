using System.Web.Mvc;

namespace Standings.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.StatusMessage = "You are not signed up to any competition. Surely there is something you feel like doing. What are you waiting to start?";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
