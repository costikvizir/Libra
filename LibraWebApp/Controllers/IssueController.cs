using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common.DataTableModels;
using LibraBll.DTOs.Issue;
using System;
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
        private readonly IUserRepository _userRepository;
        private readonly IValidator<IssuePostDTO> _issueValidator;

        public IssueController(IIssueRepository issueRepository, IValidator<IssuePostDTO> issueValidator, IPosRepository posRepository, IUserRepository userRepository)
        {
            _issueRepository = issueRepository;
            _issueValidator = issueValidator;
            _posRepository = posRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            int issueCount = await _issueRepository.GetIssueCount();
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
            List<IssueGetDTO> allIssues = await _issueRepository.GetAllIssuesAsync(parameters, CancellationToken.None);

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
            List<IssueGetDTO> issues = await _issueRepository.GetIssuesByPosIdAsync(id);

            if (!issues.Any())
                return Json(new { }, JsonRequestBehavior.AllowGet);

            return Json(issues, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> AddIssue()
        {
            var statusList = await _issueRepository.GetStatusList();
            var roles = await _userRepository.GetRoles();
            //var roles = await _userRepository.GetRolesCachedAsync();
            var issueNames = await _issueRepository.GetIssueNameList(null);
            var priorityList = await _issueRepository.GetPriorityList();

            ViewBag.Statuses = new SelectList(statusList, "Id", "IssueStatus");
            ViewBag.Roles = new SelectList(roles, "Id", "Role");
            ViewBag.IssueNames = new SelectList(issueNames, "Id", "IssueName");
            // ViewBag.IssueSubtypes = new SelectList(issueNames, "Id", "IssueName");
            // ViewBag.Problems = new SelectList(issueNames, "Id", "IssueName");
            ViewBag.PriorityList = new SelectList(priorityList, "Id", "IssuePriority");

            return View("AddIssue");
        }

        [HttpPost]
        public async Task<ActionResult> AddIssue(IssuePostDTO issue)
        {
            issue.UserCreated = User.Identity.Name;
            issue.StatusId = Convert.ToInt32(issue.Status);

            var validationResult = _issueValidator.Validate(issue);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return PartialView("AddIssue");
            }

            await _issueRepository.AddIssue(issue);

            return PartialView("AllIssues");
        }

        //TODO: solve for dropdowns in openissue view

        [HttpGet]
        public async Task<ActionResult> OpenIssue(int id)
        {
            var pos = await _posRepository.GetPosByIdAsync(id);
            var statusList = await _issueRepository.GetStatusList();
            var roles = await _userRepository.GetRoles();
            //var roles = await _userRepository.GetRolesCachedAsync();
            var issueNames = await _issueRepository.GetIssueNameList(null);
            var priorityList = await _issueRepository.GetPriorityList();

            ViewBag.PosId = pos.PosId;
            ViewBag.PosName = pos.Name;
            ViewBag.Telephone = pos.Telephone;
            ViewBag.Cellphone = pos.Cellphone;
            ViewBag.Brand = pos.Brand;
            ViewBag.Model = pos.Model;
            ViewBag.Address = pos.Address;
            ViewBag.Statuses = new SelectList(statusList, "Id", "IssueStatus");
            ViewBag.Roles = new SelectList(roles, "Id", "Role");
            ViewBag.IssueNames = new SelectList(issueNames, "Id", "IssueName");
            // ViewBag.IssueSubtypes = new SelectList(issueNames, "Id", "IssueName");
            //ViewBag.IssueProblems = new SelectList(issueNames, "Id", "IssueName");
            ViewBag.PriorityList = new SelectList(priorityList, "Id", "IssuePriority");

            //return View("OpenIssue", pos);
            return View("OpenIssue");
        }

        //[HttpPost]
        //public async Task<ActionResult> OpenIssue(IssuePostDTO issue)
        //{
        //    await _issueRepository.AddIssue(issue);
        //    //return null;
        //    // return Json(new { success = true, message = "Successfully saved" });
        //    return RedirectToAction("AllIssues");
        //}

        [HttpGet]
        public async Task<JsonResult> GetIssueSubtypes(int issueTypeId)
        {
            var subtypes = await _issueRepository.GetIssueNameList(issueTypeId);
            return Json(subtypes.Select(s => new { Id = s.Id, SubTypeName = s.IssueName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetProblemNames(int issueTypeId)
        {
            var problems = await _issueRepository.GetIssueNameList(issueTypeId);
            return Json(problems.Select(s => new { Id = s.Id, ProblemName = s.IssueName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Issue/DeleteIssue/{id}")]
        public void DeleteIssue(int id)
        {
             _issueRepository.DeleteIssue(id);
        }
    }
}