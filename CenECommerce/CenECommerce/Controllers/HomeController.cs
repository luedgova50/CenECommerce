namespace CenECommerce.Controllers
{
    
    using System.Linq;
    using System.Web.Mvc;
    using CenECommerce.Models;

    public class HomeController : Controller
    {
        private CenECommerceContext dab = new CenECommerceContext();

        public ActionResult Index()
        {
            var user =
                dab.Users.Where(
                us => us.UserName == User.Identity.Name).
                FirstOrDefault();

            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}