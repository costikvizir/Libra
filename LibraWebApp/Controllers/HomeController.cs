using LibraBll.Abstractions.Repositories;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Index()
        {
            int issueCount = await _issueRepository.GetIssueCount();
            var statusGroupCount = await _issueRepository.GetStatusGroupCount();

            ViewBag.IssueCount = issueCount;
            ViewBag.StatusGroupCount = statusGroupCount;

            return View("Index");
            //return RedirectToAction("Dashboard");
        }

        public async Task<ActionResult> Dashboard()
        {
            int issueCount = await _issueRepository.GetIssueCount();
            var statusGroupCount = await _issueRepository.GetStatusGroupCount();

            ViewBag.IssueCount = issueCount;
            ViewBag.StatusGroupCount = statusGroupCount;

            return PartialView();
        }
    }
}