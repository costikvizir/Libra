using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.Issue;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraWebApp.Controllers
{
    [Authorize(Roles = "Administrator, Technical Support, User")]
    public class IssueController : Controller
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IPosRepository _posRepository;
        private readonly IValidator<IssueDTO> _issueValidator;

        public IssueController(IIssueRepository issueRepository, IValidator<IssueDTO> issueValidator, IPosRepository posRepository)
        {
            _issueRepository = issueRepository;
            _issueValidator = issueValidator;
            _posRepository = posRepository;
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
            //List<IssueDTO> allIssues = await _issueRepository.GetAllIssuesAsync();

            //if (!allIssues.Any())
            //    return null;

            //return PartialView("AllIssues", allIssues);
            return PartialView("AllIssues");
        }

        [HttpPost]
        public async Task<JsonResult> GetAllIssuesJson(DataTablesParameters parameters = null)
        {
            List<IssueDTO> allIssues = await _issueRepository.GetAllIssuesAsync(parameters, CancellationToken.None);

            if (!allIssues.Any())
                return Json(new { }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.TotalCount,
                recordsTotal = parameters.TotalCount,
                data = allIssues
            }, JsonRequestBehavior.AllowGet);
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

        //TODO: solve for dropdowns in openissue view
        //TODO: solve for dropdowns in openissue view

        [HttpGet]
        public async Task<ActionResult> OpenIssue(int id)
        {
            var pos = await _posRepository.GetPosByIdAsync(id);
            ViewBag.PosId = pos.PosId;
            ViewBag.PosName = pos.Name;
            ViewBag.Telephone = pos.Telephone;
            ViewBag.Cellphone = pos.Cellphone;
            ViewBag.Brand = pos.Brand;
            ViewBag.Model = pos.Model;
            ViewBag.Address = pos.Address;

            //return View("OpenIssue", pos);
            return View("OpenIssue");
        }

        [HttpPost]
        public async Task<ActionResult> OpenIssue(IssueDTO issue)
        {
            await _issueRepository.AddIssue(issue);
            //return null;
           // return Json(new { success = true, message = "Successfully saved" });
            return RedirectToAction("AllIssues");
        }

        [HttpPost]
        public ActionResult DeleteIssue(int id)
        {
            _issueRepository.DeleteIssue(id);
            return RedirectToAction("Index");
        }
    }
}