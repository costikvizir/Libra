using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
	public class WelcomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}