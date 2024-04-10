using LibraBll.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IIssueRepository _issueRepository;

        public HomeController(IIssueRepository issueRepository)
        {
			_issueRepository = issueRepository;	
        }
        public ActionResult Index()
		{
			int issueCount = _issueRepository.GetIssueCount();
			ViewBag.IssueCount = issueCount;
			return View("Index");
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