using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace KeyCloakWebappSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var userPrinciple = User as ClaimsPrincipal;

            return View(userPrinciple);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Sair()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}