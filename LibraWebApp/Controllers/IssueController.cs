using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.Issue;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace LibraWebApp.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class IssueController : Controller
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IValidator<IssueDTO> _issueValidator;

        public IssueController(IIssueRepository issueRepository, IValidator<IssueDTO> issueValidator)
        {
            _issueRepository = issueRepository;
			_issueValidator = issueValidator;
        }

		[HttpGet]
		public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetIssueById(int id)
        {
            var issue =  await _issueRepository.GetIssueByIdAsync(id);
            return View(issue);
        }

		[HttpGet]
		public async Task<ActionResult> GetAllIssues()
        {
            List<IssueDTO> allIssues = await _issueRepository.GetAllIssuesAsync();
            
            if (!allIssues.Any())
				return null;

            return PartialView(allIssues);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllIssuesJson()
        {
			List<IssueDTO> allIssues = await _issueRepository.GetAllIssuesAsync();

            if (!allIssues.Any())
				return Json(new { }, JsonRequestBehavior.AllowGet);

			return Json(allIssues, JsonRequestBehavior.AllowGet);
		}

        [HttpGet]
        public ActionResult AddIssue()
        {
            return View("AddIssue");
        }

        [HttpPost]
        public async Task<ActionResult> AddIssue(IssueDTO issue)
        {
			var validationResult = _issueValidator.Validate(issue);

			if (!validationResult.IsValid)
            {
				foreach (var error in validationResult.Errors)
                {
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return PartialView();
			}

			await _issueRepository.AddIssue(issue);
            return PartialView();
		}

		[HttpGet]
		public ActionResult OpenIssue()
		{
			return View("OpenIssue");
		}
	}
}