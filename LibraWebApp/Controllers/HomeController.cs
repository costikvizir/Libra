using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return PartialView("Index");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return PartialView("About");
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return PartialView("Contact");
		}
	}
}