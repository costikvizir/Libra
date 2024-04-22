using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.User;
using LibraWebApp.ServerSidePagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LibraWebApp.ServerSidePagination;
using System;

namespace LibraWebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<AddUserDTO> _createUserValidator;
        private readonly IValidator<ModifyUserDTO> _modifyUserValidator;

        public UserController(IUserRepository userRepository, IValidator<AddUserDTO>
            createUserValidator, IValidator<ModifyUserDTO> modifyUserValidator)
        {
            _userRepository = userRepository;
            _createUserValidator = createUserValidator;
            _modifyUserValidator = modifyUserValidator;
        }

        //TODO: Put all modal windows in separate files

        [HttpGet]
        public ActionResult Index()
        {
            var user = User.Identity.Name;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetUserByName(string name)
        {
            var userFromDb = await _userRepository.GetUserByNameAsync(name);
            return View(userFromDb);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            List<GetUserDTO> allUsers = await _userRepository.GetAllUsersAsync();

            if (!allUsers.Any())
                return null;

            return PartialView(allUsers);
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetAllUsersJson(JQueryDataTableParams param)
        {
            List<GetUserDTO> allUsers = await _userRepository.GetAllUsersAsync();

            if (!allUsers.Any())
                return Json(new { }, JsonRequestBehavior.AllowGet);

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                var users = allUsers.Where(x => x.Name.Contains(param.sSearch)
                                     || x.Email.Contains(param.sSearch) 
                                     || x.Login.Contains(param.sSearch) 
                                     || x.Role.Contains(param.sSearch) 
                                     || x.Telephone.Contains(param.sSearch)).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            Func<GetUserDTO, string> orderingFunction = (x => sortColumnIndex == 0 ? x.Name :
                           sortColumnIndex == 1 ? x.Email :
                           sortColumnIndex == 2 ? x.Login :
                           sortColumnIndex == 3 ? x.Role :
                           sortColumnIndex == 4 ? x.Telephone : x.Name);

            //var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            //var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            //if (sortColumnIndex == 3)
            //{
            //    employees = sortDirection == "asc" ? employees.OrderBy(c => c.Age) : employees.OrderByDescending(c => c.Age);
            //}
            //else if (sortColumnIndex == 4)
            //{
            //    employees = sortDirection == "asc" ? employees.OrderBy(c => c.StartDate) : employees.OrderByDescending(c => c.StartDate);
            //}
            //else if (sortColumnIndex == 5)
            //{
            //    employees = sortDirection == "asc" ? employees.OrderBy(c => c.Salary) : employees.OrderByDescending(c => c.Salary);
            //}
            //else
            //{
            //    Func<Employee, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name : sortColumnIndex == 1 ? e.Position : e.Location;
            //    employees = sortDirection == "asc" ? employees.OrderBy(orderingFunction) : employees.OrderByDescending(orderingFunction);
            //}

            //var displayResult = employees.Skip(param.iDisplayStart)
            //   .Take(param.iDisplayLength).ToList();
            //var totalRecords = employees.Count();
            //return Json(new
            //{
            //    param.sEcho,
            //    iTotalRecords = totalRecords,
            //    iTotalDisplayRecords = totalRecords,
            //    aaData = displayResult
            //}, JsonRequestBehavior.AllowGet);

            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return View("AddUser");
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserDTO user)
        {
            var results = _createUserValidator.Validate(user);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return PartialView();
            }
            await _userRepository.CreateUser(user);

            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> UpdateUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return PartialView("~/Views/User/_Edit.cshtml", user);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(ModifyUserDTO user)
        {
            var modifiedUser = new ModifyUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                Role = user.Role,
                Password = user.Password,
                IsActive = user.IsActive,
                Telephone = user.Telephone,
            };

            var results = _modifyUserValidator.Validate(modifiedUser);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return PartialView("GetAllUsers");
            }

            _userRepository.UpdateUser(user);

            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Role");

            return PartialView("GetAllUsers");
        }

        [HttpPost]
        [Route("User/DeleteUser/{id}")]
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}