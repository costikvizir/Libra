using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.Issue;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            int issueCount = _issueRepository.GetIssueCount();
            ViewBag.IssueCount = issueCount;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetIssueById(int id)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(id);
            return PartialView("~/Views/Issue/_IssueDetails.cshtml", issue);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllIssues()
        {
            List<IssueDTO> allIssues = await _issueRepository.GetAllIssuesAsync();

            if (!allIssues.Any())
                return null;

            return PartialView("AllIssues", allIssues);
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
        public async Task<JsonResult> GetIssuesJsonByPosId(int id)
        {
            List<IssueDTO> issues = await _issueRepository.GetIssuesByPosIdAsync(id);

            if (!issues.Any())
                return Json(new { }, JsonRequestBehavior.AllowGet);

            return Json(issues, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddIssue()
        {
            return View("AddIssue");
        }

        [HttpPost]
        public async Task<ActionResult> AddIssue(IssueDTO issue)
        {
            issue.UserCreated = User.Identity.Name;
            //var validationResult = _issueValidator.Validate(issue);

            //if (!validationResult.IsValid)
            //{
            //    foreach (var error in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return PartialView("AddIssue");
            //}
            try
            {
                await _issueRepository.AddIssue(issue);
            }
            catch (System.Exception)
            {
                throw;
            }
            return PartialView("AddIssue");
        }

        [HttpGet]
        public ActionResult OpenIssue()
        {
            return View("OpenIssue");
        }

        [HttpPost]
        public ActionResult DeleteIssue(int id)
        {
            _issueRepository.DeleteIssue(id);
            return RedirectToAction("Index");
        }
    }
}